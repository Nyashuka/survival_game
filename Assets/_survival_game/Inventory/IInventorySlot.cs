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
        IItem Item { get; }
        void Clear();
        void SetItem(IItem item);
    }
}