using System;

namespace _survival_game.Inventory
{
    public interface IItem : IEquatable<IItem>
    {
        public int Amount { get; }
        public IItemInfo ItemInfo { get; }
        public void ChangeAmount(int newAmount);
        public void AddAmount(int amountToAdd);
        public IItem Clone();
    }
}
