using System;
using _survival_game.Inventory.InventoryUI;
using Unity.Collections;

namespace _survival_game.Inventory
{
    public interface IInventorySlot
    {
        bool IsFull { get; }
        bool IsEmpty { get; }
        int AmountItems { get; }
        IInventoryItem Item { get; }
        void AddAmount(int amount);
        void Clear();
        void SetItem(IInventoryItem item);
        public event Action<IInventoryItem> ItemAdded;
    }
}