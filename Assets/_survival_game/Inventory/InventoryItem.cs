namespace _survival_game.Inventory
{
    public class InventoryItem : IInventoryItem
    {
        public InventoryItem(int amount, IInventoryItemInfo itemInfo)
        {
            Amount = amount;
            ItemInfo = itemInfo;
        }

        public int Amount { get; set; }
     
        public IInventoryItemInfo ItemInfo { get; }
        
        
        public IInventoryItem Clone()
        {
            return new InventoryItem(Amount, ItemInfo);
        }
    }
}