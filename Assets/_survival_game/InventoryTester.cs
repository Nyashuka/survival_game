using _survival_game.Inventory;
using _survival_game.Inventory.Scripts;
using _survival_game.Inventory.Scripts.Interfaces;
using _survival_game.Inventory.Scripts.InventoryUI;
using _survival_game.ScriptableObjects;
using UnityEngine;

namespace _survival_game
{
    public class InventoryTester : MonoBehaviour
    {
        [SerializeField] private InventoryWindowSettings test;
        
        [SerializeField] private Food greenApple;
        [SerializeField] private Food redApple;
        [SerializeField] private InventoryUI inventory;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                IItem item = new Item(1, greenApple);
                inventory.PutItem(item);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                IItem item = new Item(1, redApple);
                inventory.PutItem(item);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                IItem item = new Item(1, redApple);
                inventory.PutItemIntoSlot(item);
            }
        }
    }
}