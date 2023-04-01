using System.Collections.Generic;
using _survival_game.Inventory.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace _survival_game.Inventory.InventoryUI
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(Image))]
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Image inventoryPanel;
        [SerializeField] private GridLayoutGroup inventoryGrid;
        [SerializeField] private InventorySlotUI slotPrefab;
        [SerializeField] private int slotsRowCount;
        [SerializeField] private int slotsColCount;
        [SerializeField] private int padding = 15;
        [SerializeField] private int spacing = 15;
        
        private IInventory _inventory;
        private List<InventorySlotUI> _slotsUI;

        private float _height;
        private float _width;

        private void Start()
        {
            InitSize();
            InitInventory();
            GenerateUI();
        }
        
        private void InitSize()
        {
            inventoryGrid.padding.bottom = padding;
            inventoryGrid.padding.left = padding;
            inventoryGrid.padding.top = padding;
            inventoryGrid.padding.right = padding;

            inventoryGrid.spacing = new Vector2(spacing, spacing);

            _height = inventoryPanel.rectTransform.rect.height - padding * 2 - spacing * (slotsRowCount - 1);
            _width = inventoryPanel.rectTransform.rect.width - padding * 2 - spacing * (slotsColCount - 1);
        }

        private void InitInventory()
        {
            _inventory = new Inventory(slotsColCount * slotsRowCount);
            _inventory.InventoryStateChanged += OnInventoryStateChanged;
        }
        
        private void GenerateUI()
        {
            float slotWidth = _width / slotsColCount;
            float slotHeight = _height / slotsRowCount;

            inventoryGrid.cellSize = new Vector2(slotWidth, slotHeight);
            _slotsUI = new List<InventorySlotUI>();

            var slots = _inventory.GetAllSlots();

            foreach (var slot in slots)
            {
                var slotUI = Instantiate(slotPrefab, inventoryPanel.transform);
                slotUI.SetSlot(slot);
                slotUI.ItemDropped += OnTransitItem;
                _slotsUI.Add(slotUI);
            }
        }

        private void OnInventoryStateChanged()
        {
            foreach (var slotUI in _slotsUI)
            {
                slotUI.UpdateData();
            }
        }
        
        private void OnTransitItem(IInventorySlot slotFrom, IInventorySlot slotTo)
        {
            _inventory.TransitItemInOtherSlot(slotFrom, slotTo);
        }
        
        /*
         * !!!! All methods below are just for the test,
         * so as not to create a separate class for the test,
         * will be removed very soon once I come up with the control logic!!!!
         */
        public void PutItem(IItem item)
        {
            _inventory.TryAddItem(item);
        }

        public void PutItemIntoSlot(IItem item)
        {
            List<IInventorySlot> slots = _inventory.GetAllSlots();
            _inventory.TryAddItemToSlot(slots[14], item);
        }

        

        

       
    }
}