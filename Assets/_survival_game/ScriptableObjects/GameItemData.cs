using _survival_game.Inventory;
using _survival_game.Inventory.Scripts.Interfaces;
using UnityEngine;

namespace _survival_game.ScriptableObjects
{
    public class GameItemData :  ScriptableObject, IItemInfo
    {
        [SerializeField] private string id;
        [SerializeField] private string title;
        [SerializeField] private int maxCountsInSlot;
        [SerializeField] private Sprite spriteIcon;

        public string Id => id;
        public string Title => title;
        public int MaxCountInStack => maxCountsInSlot;
        public Sprite Icon => spriteIcon;
    }
}