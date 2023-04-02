using _survival_game.Inventory.Scripts.Interfaces;

namespace _survival_game.Inventory.Scripts
{
    public class InventorySlot : IInventorySlot
    {
        public bool IsFull => !IsEmpty && AmountItems == MaxCountInStack;
        public bool IsEmpty => Item == null;
        public int AmountItems => Item?.Amount ?? 0;
        public IItem Item { get; private set; }
        public int MaxCountInStack => Item?.MaxCountInStack ?? 0; 
        
        public void Clear()
        {
            Item = null;
        }

        public void SetItem(IItem item)
        {
            Item = item;
        }
    }
}