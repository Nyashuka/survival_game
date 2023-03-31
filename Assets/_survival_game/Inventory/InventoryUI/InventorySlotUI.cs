using UnityEngine;
using UnityEngine.EventSystems;

namespace _survival_game.Inventory.InventoryUI
{
    public class InventorySlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] private InventoryItemUI _itemUIPrefab;

        public void PutItem(IInventoryItem item)
        {
            Instantiate(_itemUIPrefab, transform).SetItem(item);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            var otherItemTransform = eventData.pointerDrag.transform;

            otherItemTransform.SetParent(transform);
            otherItemTransform.localPosition = Vector3.zero;
        }
    }
}