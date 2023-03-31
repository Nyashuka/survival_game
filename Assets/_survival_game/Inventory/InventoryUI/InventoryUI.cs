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

            GenerateInventory();
        }

        private void GenerateInventory()
        {
            float slotWidth = _width / _slotsColCount;
            float slotHeight = _height / _slotsRowCount;

            _inventoryGrid.cellSize = new Vector2(slotWidth, slotHeight);

            List<IInventorySlot> slots = new List<IInventorySlot>();

            for (int i = 0; i < _slotsColCount * _slotsRowCount; i++)
            {
                IInventorySlot slot = new InventorySlot(Instantiate(_slotPrefab, _inventoryPanel.transform));
                slots.Add(slot);
            }

            _inventory = new Inventory(slots);
        }

        private void InitInventory()
        {
            _inventory = new Inventory(_slotsColCount * _slotsRowCount);
        }

        public void PutItem(IInventoryItem item)
        {
            _inventory.TryAddItem(item);
        }

        private void GenerateUI()
        {
            float slotWidth = _width / _slotsColCount;
            float slotHeight = _height / _slotsRowCount;

            _inventoryGrid.cellSize = new Vector2(slotWidth, slotHeight);

            for (int i = 0; i < _slotsRowCount * _slotsColCount; i++)
            {
                Instantiate(_slotPrefab, _inventoryPanel.transform);
            }
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