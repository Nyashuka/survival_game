using _survival_game.Inventory.Interfaces;

namespace _survival_game.Inventory
{
    public class Item : IItem
    {
        private int _amount;
        public Item(int amount, IItemInfo itemInfo)
        {
            _amount = amount;
            ItemInfo = itemInfo;
        }

        public int Amount => _amount;
        
        public IItemInfo ItemInfo { get; }

        public void ChangeAmount(int newAmount)
        {
            _amount = newAmount;
        }

        public void AddAmount(int amountToAdd)
        {
            _amount += amountToAdd;
        }

        public IItem Clone()
        {
            return new Item(Amount, ItemInfo);
        }

        public bool Equals(IItem other)
        {
            if (other == null)
                return false;

            return ItemInfo.Id == other.ItemInfo.Id;
        }
    }
}