using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace _survival_game.Inventory
{
    public class Inventory : IInventory
    {
        private List<IInventorySlot> _slots;
        public int SlotsAmount { get; }

        public event Action<IInventorySlot> CreatedSlot; 

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

        public Inventory(List<IInventorySlot> slots)
        {
            SlotsAmount = slots.Count;
            _slots = slots;
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
            if (slotTo.IsEmpty)
            {
                slotTo.SetItem(slotFrom.Item.Clone());
                slotFrom.Clear();
            }
        }

        public void Remove(IInventoryItem item, int amount = 1)
        {
        }
    }
}