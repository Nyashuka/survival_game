using UnityEngine;

namespace _survival_game.Inventory.Scripts.Interfaces
{
    public interface IItemInfo
    {
        string Id { get; }
        string Title { get; }
        int MaxCountInStack { get; }
        Sprite Icon { get; }
    }
}