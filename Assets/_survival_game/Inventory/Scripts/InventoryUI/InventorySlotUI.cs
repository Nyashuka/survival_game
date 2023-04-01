using System;
using _survival_game.Inventory.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _survival_game.Inventory.InventoryUI
{
    public class InventorySlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] private InventoryItemUI itemUI;
        public IInventorySlot Slot { get; private set; }
        public event Action<IInventorySlot, IInventorySlot> ItemDropped;

        public void SetSlot(IInventorySlot slot)
        {
            Slot = slot;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out InventoryItemUI foundItemUI))
            {
                InventorySlotUI slotFrom = foundItemUI.SlotUI;
            
                ItemDropped?.Invoke(slotFrom.Slot, Slot);
            }
        }

        public void UpdateData()
        {
            if (Slot != null)
                itemUI.SetItem(this);
        }
    }
}