using System;
using System.Collections.Generic;
using _survival_game.Inventory.Scripts.Interfaces;

namespace _survival_game.Inventory
{
    public class Inventory : IInventory
    { 
        private readonly List<IInventorySlot> _slots;
        public event Action InventoryStateChanged;
        
        public Inventory(int slotsAmount)
        {
            _slots = new List<IInventorySlot>(slotsAmount);

            for (int i = 0; i < slotsAmount; i++)
            {
                _slots.Add(new InventorySlot());
            }
        }

        public bool TryAddItem(IItem item)
        {
            IInventorySlot slotWithSameItem = _slots.Find(slot => !slot.IsEmpty &&
                                                                        slot.Item.Equals(item) && 
                                                                        !slot.IsFull);

            if (slotWithSameItem != null)
                return TryAddItemToSlot(slotWithSameItem, item);

            IInventorySlot emptySlot = _slots.Find(slot => slot.IsEmpty);

            if (emptySlot != null)
                return TryAddItemToSlot(emptySlot, item);

            return false;
        }

        public bool TryAddItemToSlot(IInventorySlot slot, IItem item)
        {  
            if (slot.AmountItems + item.Amount <= item.ItemInfo.MaxCountInStack)
            {
                if (slot.IsEmpty)
                {
                    slot.SetItem(item.Clone());
                }
                else
                {
                    slot.Item.AddAmount(item.Amount);
                }
                InventoryStateChanged?.Invoke();
                return true;
            }

            int amountToAdd = item.ItemInfo.MaxCountInStack - slot.AmountItems;
            int leftItemsAmount = (slot.AmountItems + item.Amount) - item.ItemInfo.MaxCountInStack;
            
            slot.Item.AddAmount(amountToAdd);
            item.ChangeAmount(leftItemsAmount);

            return TryAddItem(item);
        }

        public void TransitItemInOtherSlot(IInventorySlot slotFrom, IInventorySlot slotTo)
        {
            if (
                slotTo.IsFull || 
                slotFrom.IsEmpty ||
                slotFrom == slotTo ||
                (!slotTo.IsEmpty && !slotTo.Item.Equals(slotFrom.Item))
               )
                return;
            
            if (slotTo.IsEmpty)
            {
                slotTo.SetItem(slotFrom.Item);
                slotFrom.Clear();
                InventoryStateChanged?.Invoke();
                return;
            }
            
            bool fits = slotFrom.AmountItems + slotTo.AmountItems <= slotFrom.Item.ItemInfo.MaxCountInStack;
            int amountToTransit = fits ? slotFrom.AmountItems : slotFrom.Item.ItemInfo.MaxCountInStack - slotTo.AmountItems;
            int amountLeft = slotFrom.AmountItems - amountToTransit;
            slotTo.Item.AddAmount(amountToTransit);
            
            if(fits) slotFrom.Clear();
            else slotFrom.Item.ChangeAmount(amountLeft);

            InventoryStateChanged?.Invoke();
        }


        public List<IInventorySlot> GetAllSlots()
        {
            return _slots;
        }
    }
}