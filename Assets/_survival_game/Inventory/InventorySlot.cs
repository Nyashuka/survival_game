namespace _survival_game.Inventory
{
    public class InventorySlot : IInventorySlot
    {
        public bool IsFull => !IsEmpty && AmountItems == Item.ItemInfo.MaxCountInStack;
        public bool IsEmpty => Item == null;
        public int AmountItems => Item?.Amount ?? 0;
        public IItem Item { get; private set; }
        
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