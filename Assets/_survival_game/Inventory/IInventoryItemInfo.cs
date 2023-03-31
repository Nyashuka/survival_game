using UnityEngine;

namespace _survival_game.Inventory
{
    public interface IInventoryItemInfo
    {
        string Id { get; }
        string Title { get; }
        int MaxCountInStack { get; }
        Sprite Icon { get; }
    }
}