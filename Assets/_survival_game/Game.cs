using System;
using _survival_game.Inventory;
using _survival_game.Inventory.InventoryUI;
using _survival_game.ScriptableObjects;
using UnityEngine;

namespace _survival_game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Food _greenApple;
        [SerializeField] private Food _redApple;
        [SerializeField] private InventoryUI _inventory;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                IInventoryItem item = new InventoryItem(1, _greenApple);
                _inventory.PutItem(item);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                IInventoryItem item = new InventoryItem(1, _redApple);
                _inventory.PutItem(item);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                IInventoryItem item = new InventoryItem(1, _redApple);
                _inventory.PutItemIntoSlot(item);
            }
        }
    }
}