using System;
using System.Collections.Generic;

namespace _survival_game.Inventory
{
    public interface IInventory
    {
        public event Action InventoryStateChanged;
        bool TryAddItem(IItem item);
        bool TryAddItemToSlot(IInventorySlot slot, IItem item);
        void TransitItemInOtherSlot(IInventorySlot slotFrom, IInventorySlot slotTo);
        List<IInventorySlot> GetAllSlots();
    }
}