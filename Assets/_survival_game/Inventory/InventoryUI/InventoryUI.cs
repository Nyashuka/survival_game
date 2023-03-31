using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _survival_game.Inventory.InventoryUI
{
    public class InventoryUI : MonoBehaviour
    {
        private const int PADDING = 15;
        private const int SPACING = 15;
        private IInventory _inventory;
        private List<InventorySlotUI> _slotsUI;

        [SerializeField] private Image _inventoryPanel;
        [SerializeField] private GridLayoutGroup _inventoryGrid;
        [SerializeField] private InventorySlotUI _slotPrefab;
        [SerializeField] private int _slotsRowCount;
        [SerializeField] private int _slotsColCount;

        private float _height;
        private float _width;

        private void Start()
        {
            InitSize();
            InitInventory();
            GenerateUIWithMath();
        }

        private void InitInventory()
        {
            _inventory = new Inventory(_slotsColCount * _slotsRowCount);
            _inventory.InventoryStateChanged += OnInventoryStateChanged;
        }

        private void OnInventoryStateChanged()
        {
            foreach (var slotUI in _slotsUI)
            {
                slotUI.UpdateData();
            }
        }

        public void PutItem(IInventoryItem item)
        {
            _inventory.TryAddItem(item);
        }
        
        public void PutItemIntoSlot(IInventorySlot slot,IInventoryItem item)
        {
            _inventory.TryAddItemToSlot(slot, item);
        }
        private void GenerateUIWithMath()
        {
            float slotWidth = _width / _slotsColCount;
            float slotHeight = _height / _slotsRowCount;

            _inventoryGrid.cellSize = new Vector2(slotWidth, slotHeight);
            _slotsUI = new List<InventorySlotUI>();

            var slots = _inventory.GetAllSlots();

            float posX = PADDING;
            float posY = PADDING;
            
            foreach (var slot in slots)
            {
                var slotUI = Instantiate(_slotPrefab, _inventoryPanel.transform);
                slotUI.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0);
                slotUI.SetSlot(slot);
                slotUI.ItemDropped += OnTransitItem;
                _slotsUI.Add(slotUI);
                posX += SPACING + slotWidth;
            }
        }

        private void GenerateUI()
        {
            float slotWidth = _width / _slotsColCount;
            float slotHeight = _height / _slotsRowCount;

            _inventoryGrid.cellSize = new Vector2(slotWidth, slotHeight);
            _slotsUI = new List<InventorySlotUI>();

            var slots = _inventory.GetAllSlots();
            
            foreach (var slot in slots)
            {
                var slotUI = Instantiate(_slotPrefab, _inventoryPanel.transform);
                slotUI.SetSlot(slot);
                slotUI.ItemDropped += OnTransitItem;
                _slotsUI.Add(slotUI);
            }
        }

        private void OnTransitItem(IInventorySlot arg1, IInventorySlot arg2)
        {
            _inventory.TransitItemInOtherSlot(arg1,arg2);
        }

        private void InitSize()
        {
            _inventoryGrid.padding.bottom = PADDING;
            _inventoryGrid.padding.left = PADDING;
            _inventoryGrid.padding.top = PADDING;
            _inventoryGrid.padding.right = PADDING;

            _inventoryGrid.spacing = new Vector2(SPACING, SPACING);

            _height = _inventoryPanel.rectTransform.rect.height - PADDING * 2 - SPACING * (_slotsRowCount - 1);
            _width = _inventoryPanel.rectTransform.rect.width - PADDING * 2 - SPACING * (_slotsColCount - 1);
            
            Debug.Log(_width);
            Debug.Log(_height);
        }
    }
}