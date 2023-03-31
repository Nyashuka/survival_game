using System;
using System.Collections.Generic;

namespace _survival_game.Inventory
{
    public interface IInventory
    {
        public event Action InventoryStateChanged;
        int SlotsAmount { get; }
        bool TryAddItem(IInventoryItem item);
        bool TryAddItemToSlot(IInventorySlot slot, IInventoryItem item);
        void TransitItemInOtherSlot(IInventorySlot slotFrom, IInventorySlot slotTo);
        void Remove(IInventoryItem item, int amount = 1);
        List<IInventorySlot> GetAllSlots();
    }
}