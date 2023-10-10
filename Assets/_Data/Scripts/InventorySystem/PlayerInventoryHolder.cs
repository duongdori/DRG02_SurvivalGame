using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InventorySystem
{
    public class PlayerInventoryHolder : InventoryHolder
    {
        [SerializeField] protected int secondaryInventorySize;
        
        [SerializeField] protected InventorySystem secondaryInventorySystem;
        public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

        public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

        protected override void Awake()
        {
            base.Awake();

            secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
            
            primaryInventorySystem.OnInventoryItemRemaining += OnPrimaryInventoryItemRemaining;
            secondaryInventorySystem.OnInventoryItemRemaining += OnSecondInventoryItemRemaining;
        }

        private void OnDestroy()
        {
            primaryInventorySystem.OnInventoryItemRemaining -= OnPrimaryInventoryItemRemaining;
            secondaryInventorySystem.OnInventoryItemRemaining -= OnSecondInventoryItemRemaining;
        }

        private void OnPrimaryInventoryItemRemaining(InventoryItemData itemData, int amount)
        {
            if (secondaryInventorySystem.CanAddItem(itemData))
            {
                secondaryInventorySystem.AddToInventory(itemData, amount);
            }
            else
            {
                // TODO: inventory is full - drop item on ground
                DropItem(itemData, amount);
            }
        }
        private void OnSecondInventoryItemRemaining(InventoryItemData itemData, int amount)
        {
            // TODO: inventory is full - drop item on ground
            DropItem(itemData, amount);
        }

        private void Update()
        {
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
            }
        }

        public bool AddToInventory(InventoryItemData itemData, int amount)
        {
            if (primaryInventorySystem.CanAddItem(itemData))
            {
                primaryInventorySystem.AddToInventory(itemData, amount);
                return true;
            }

            if (secondaryInventorySystem.CanAddItem(itemData))
            {
                secondaryInventorySystem.AddToInventory(itemData, amount);
                return true;
            }

            return false;
        }

        private void DropItem(InventoryItemData itemData, int amount)
        {
            Vector3 pos = transform.position + transform.forward * 3f;
            GameObject item = Instantiate(itemData.itemPrefab, pos, Quaternion.identity);
            ItemPickUp itemPickUp = item.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                itemPickUp.UpdateItem(itemData, amount);
            }
        }
    }
}
