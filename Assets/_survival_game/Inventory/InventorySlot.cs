using System;
using _survival_game.Inventory.InventoryUI;

namespace _survival_game.Inventory
{
    public class InventorySlot : IInventorySlot
    {
        private readonly InventorySlotUI _slotUI;
        public bool IsFull { get; }
        public bool IsEmpty => Item == null;
        public int AmountItems => Item == null ? 0 : Item.Amount;
        public IInventoryItem Item { get; private set; }
        

        public event Action<IInventoryItem> ItemAdded;
        public InventorySlot()
        {
            IsFull = false;
            Item = null;
        }
        
        public InventorySlot(InventorySlotUI slotUI) : this()
        {
            _slotUI = slotUI;
        }
        
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
            _slotUI.PutItem(item);
        }

        
    }
}