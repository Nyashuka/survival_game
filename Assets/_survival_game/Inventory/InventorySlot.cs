using System;
using _survival_game.Inventory.InventoryUI;

namespace _survival_game.Inventory
{
    public class InventorySlot : IInventorySlot
    {
        public bool IsFull => !IsEmpty && AmountItems == Item.ItemInfo.MaxCountInStack;
        public bool IsEmpty => Item == null;
        public int AmountItems => Item == null ? 0 : Item.Amount;
        public IInventoryItem Item { get; private set; }
        
        public event Action<IInventoryItem> ItemAdded;

        public void AddAmount(int amount)
        {
            Item.Amount += amount;
        }

        public void Clear()
        {
            Item = null;
        }

        public void SetItem(IInventoryItem item)
        {
            Item = item;
        }

        
    }
}