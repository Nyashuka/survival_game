using _survival_game.Inventory;
using _survival_game.Inventory.Interfaces;
using UnityEngine;

namespace _survival_game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Create new item info")]
    public class Food : ScriptableObject, IItemInfo
    {
        [SerializeField] private string _id;
        [SerializeField] private string _title;
        [SerializeField] private int _maxCountsInSlot;
        [SerializeField] private Sprite _spriteIcon;

        public string Id => _id;
        public string Title => _title;
        public int MaxCountInStack => _maxCountsInSlot;
        public Sprite Icon => _spriteIcon;
    }
}