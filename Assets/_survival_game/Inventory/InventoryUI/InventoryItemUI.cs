using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _survival_game.Inventory.InventoryUI
{
    public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _textAmount;
        [SerializeField] private CanvasGroup _imageCanvaseGroup;
        [SerializeField] private CanvasGroup _textCanvasGroup;
        
        public InventorySlotUI SlotUI { get; set; }

        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private IInventoryItem _item;

        private Transform _oldParent;

        private void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();

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
            _canvasGroup.interactable = !isDisabled;
            _canvasGroup.blocksRaycasts = !isDisabled;
            _imageCanvaseGroup.alpha = isDisabled ? 0 : 1;
            _textCanvasGroup.alpha = isDisabled ? 0 : 1;
            _textAmount.text = isDisabled ? "" : _item.Amount == 1 ? "" : _item.Amount.ToString();
            _image.sprite = isDisabled ? null : _item.ItemInfo.Icon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
           /*var slotTransform = transform.parent;
           slotTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;*/
            //transform.parent = _canvas.transform;

            _canvasGroup.blocksRaycasts = false;
            _oldParent = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            /*_rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;*/
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.parent = _oldParent;
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}