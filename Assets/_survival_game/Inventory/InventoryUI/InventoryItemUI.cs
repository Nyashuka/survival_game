using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _survival_game.Inventory.InventoryUI
{
    public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _image;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private IInventoryItem _item;

        private void Start()
        {        
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetItem(IInventoryItem item)
        {
            _item = item;
            _image.sprite = item.ItemInfo.Icon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var slotTransform = _rectTransform.parent;
            slotTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
            transform.parent = _canvas.transform;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            //_rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
        }


    }
}