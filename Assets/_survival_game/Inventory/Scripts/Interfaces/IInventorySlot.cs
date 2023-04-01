namespace _survival_game.Inventory.Scripts.Interfaces
{
    public interface IInventorySlot
    {
        bool IsFull { get; }
        bool IsEmpty { get; }
        int AmountItems { get; }
        IItem Item { get; }
        void Clear();
        void SetItem(IItem item);
    }
}