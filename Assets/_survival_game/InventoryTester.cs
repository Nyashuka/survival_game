using System;
using _survival_game.Inventory;
using _survival_game.Inventory.InventoryUI;
using _survival_game.ScriptableObjects;
using UnityEngine;

namespace _survival_game
{
    public class InventoryTester : MonoBehaviour
    {
        [SerializeField] private Food _greenApple;
        [SerializeField] private Food _redApple;
        [SerializeField] private InventoryUI _inventory;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                IItem item = new Item(1, _greenApple);
                _inventory.PutItem(item);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                IItem item = new Item(1, _redApple);
                _inventory.PutItem(item);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                IItem item = new Item(1, _redApple);
                _inventory.PutItemIntoSlot(item);
            }
        }
        
    }
}