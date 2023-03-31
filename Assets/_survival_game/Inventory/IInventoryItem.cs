using Unity.VisualScripting;

namespace _survival_game.Inventory
{
    public interface IInventoryItem
    {
        public int Amount { get; set; }

        public IInventoryItem Clone();
        
        public IInventoryItemInfo ItemInfo { get; }
    }
}
