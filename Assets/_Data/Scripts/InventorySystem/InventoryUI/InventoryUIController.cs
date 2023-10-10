using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace InventorySystem
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private DynamicInventoryDisplay chestPanel;
        [SerializeField] private DynamicInventoryDisplay playerBackpackPanel;

        private void Awake()
        {
            chestPanel.gameObject.SetActive(false);
            playerBackpackPanel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
            PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
        }
        private void OnDisable()
        {
            InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
            PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
        }

        private void Update()
        {
            if (chestPanel.gameObject.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                chestPanel.gameObject.SetActive(false);
            }
            
            if (playerBackpackPanel.gameObject.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                playerBackpackPanel.gameObject.SetActive(false);
            }
        }

        private void DisplayInventory(InventorySystem invToDisplay)
        {
            chestPanel.gameObject.SetActive(true);
            chestPanel.RefreshDynamicInventory(invToDisplay);
        }
        
        private void DisplayPlayerBackpack(InventorySystem invToDisplay)
        {
            playerBackpackPanel.gameObject.SetActive(true);
            playerBackpackPanel.RefreshDynamicInventory(invToDisplay);
        }
    }
}