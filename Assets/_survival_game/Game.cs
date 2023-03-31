using System;
using _survival_game.Inventory;
using _survival_game.Inventory.InventoryUI;
using _survival_game.ScriptableObjects;
using UnityEngine;

namespace _survival_game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Food _item;
        [SerializeField] private InventoryUI _inventory;
        
        private void Start()
        {
            
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IInventoryItem item = new InventoryItem(1, _item);
                _inventory.PutItem(item);
            }
        }
    }
}