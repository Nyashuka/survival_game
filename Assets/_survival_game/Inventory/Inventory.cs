using System;
using System.Collections.Generic;

namespace _survival_game.Inventory
{
    public class Inventory : IInventory
    { 
        private List<IInventorySlot> _slots;
        public event Action InventoryStateChanged;
        public int SlotsAmount { get; }
        public Inventory(int slotsAmount)
        {
            SlotsAmount = slotsAmount;
            _slots = new List<IInventorySlot>(slotsAmount);

            for (int i = 0; i < slotsAmount; i++)
            {
                IInventorySlot slot = new InventorySlot();
                _slots.Add(new InventorySlot());
            }
        }
        
        private bool IsSameItems(IInventoryItem item1, IInventoryItem item2)
        {
            return item1.ItemInfo.Id == item2.ItemInfo.Id;
        }

        public bool TryAddItem(IInventoryItem item)
        {
            IInventorySlot slotWithSameItem = _slots.Find(slot => !slot.IsEmpty &&
                                                                  IsSameItems(slot.Item, item));

            if (slotWithSameItem != null)
                return TryAddItemToSlot(slotWithSameItem, item);

            IInventorySlot emptySlot = _slots.Find(slot => slot.IsEmpty);

            if (emptySlot != null)
                return TryAddItemToSlot(emptySlot, item);

            return false;
        }

        public bool TryAddItemToSlot(IInventorySlot slot, IInventoryItem item)
        {  
            if (slot.AmountItems + item.Amount <= item.ItemInfo.MaxCountInStack)
            {
                if (slot.IsEmpty)
                {
                    slot.SetItem(item.Clone());
                }
                else
                {
                    slot.AddAmount(item.Amount);
                }
                InventoryStateChanged?.Invoke();
                return true;
            }

            int amountToAdd = item.ItemInfo.MaxCountInStack - slot.AmountItems;
            int leftItemsAmount = (slot.AmountItems + item.Amount) - item.ItemInfo.MaxCountInStack;

            slot.AddAmount(amountToAdd);
            item.Amount = leftItemsAmount;

            return TryAddItem(item);
        }

        public void TransitItemInOtherSlot(IInventorySlot slotFrom, IInventorySlot slotTo)
        {
            if (slotTo.IsFull || slotFrom.IsEmpty ||
                (!slotTo.IsEmpty && !IsSameItems(slotTo.Item, slotFrom.Item)))
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
            slotTo.Item.Amount += amountToTransit;
            
            if(fits) slotFrom.Clear();
            else slotFrom.Item.Amount = amountLeft;

            InventoryStateChanged?.Invoke();
        }

        public void Remove(IInventoryItem item, int amount = 1)
        {
            throw new NotImplementedException();
        }

        public List<IInventorySlot> GetAllSlots()
        {
            return _slots;
        }
    }
}