using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _survival_game.Inventory.InventoryUI
{
    public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _imageCanvaseGroup;
        [SerializeField] private CanvasGroup _textCanvasGroup;

        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private IInventoryItem _item;

        private void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();

            Clear();
        }

        public void SetItem(IInventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                Clear();
                return;
            }

            _item = slot.Item;
            _image.sprite = _item.ItemInfo.Icon;

            _imageCanvaseGroup.alpha = 1;
            _textCanvasGroup.alpha = 1;
        }

        private void Clear()
        {
            _imageCanvaseGroup.alpha = 0;
            _textCanvasGroup.alpha = 0;
            _image.sprite = null;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var slotTransform = _rectTransform.parent;
            slotTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
            //transform.parent = _canvas.transform;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //transform.position = Input.mousePosition;
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}