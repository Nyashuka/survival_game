using _survival_game.Inventory.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _survival_game.Inventory.Scripts.InventoryUI
{
    public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private Text textAmount;
        [SerializeField] private CanvasGroup imageCanvasGroup;
        [SerializeField] private CanvasGroup textCanvasGroup;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private IItem _item;
        private Transform _parent;

        public InventorySlotUI SlotUI { get; private set; }

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            IsDisabled(true);
        }

        public void SetItem(InventorySlotUI slotUI)
        {
            SlotUI = slotUI;
            transform.SetParent(slotUI.transform);
            
            if (slotUI.Slot.IsEmpty)
            {
                IsDisabled(true);
                return;
            }

            _item = slotUI.Slot.Item;
            
            IsDisabled(false);
        }

        private void IsDisabled(bool isDisabled)
        {
            canvasGroup.interactable = !isDisabled;
            canvasGroup.blocksRaycasts = !isDisabled;
            imageCanvasGroup.alpha = isDisabled ? 0 : 1;
            textCanvasGroup.alpha = isDisabled ? 0 : 1;
            textAmount.text = isDisabled ? "" : _item.Amount == 1 ? "" : _item.Amount.ToString();
            image.sprite = isDisabled ? null : _item.ItemInfo.Icon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
            _parent = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.parent = _parent;
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
        }
    }
}