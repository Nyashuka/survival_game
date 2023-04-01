using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _survival_game.Inventory.InventoryUI
{
    public class InventorySlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] private InventoryItemUI _itemUI;
        public IInventorySlot Slot { get; private set; }
        public event Action<IInventorySlot, IInventorySlot> ItemDropped;

        public void SetSlot(IInventorySlot slot)
        {
            Slot = slot;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            InventoryItemUI itemUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();
            InventorySlotUI slotFrom = itemUI.SlotUI;
            
            ItemDropped?.Invoke(slotFrom.Slot, Slot);
            
            /*UpdateData();
            slotFrom.UpdateData();*/
        }

        public void UpdateData()
        {
            if (Slot != null)
                _itemUI.SetItem(this);
        }
    }
}