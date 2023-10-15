using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{
    public class StaticInventoryDisplay : InventoryDisplay
    {
        [SerializeField] private InventoryHolder inventoryHolder;
        [SerializeField] private InventorySlotUI[] slots;

        public static UnityAction<InventorySlotUI> OnSlotIndicatorChanged;
        protected override void Start()
        {
            base.Start();

            if (inventoryHolder != null)
            {
                inventorySystem = inventoryHolder.PrimaryInventorySystem;
                inventorySystem.OnInventorySlotChanged += UpdateSlot;
            }
            else
            {
                Debug.LogWarning("No inventory assigned to " + gameObject);
            }
            
            AssignSlot(inventorySystem);
        }

        public override void AssignSlot(InventorySystem invToDisplay)
        {
            slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

            if (slots.Length != inventorySystem.InventorySize)
            {
                Debug.Log("Inventory slots out of sync on " + gameObject);
            }
            
            for (int i = 0; i < inventorySystem.InventorySize; i++)
            {
                slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
                slots[i].Init(inventorySystem.InventorySlots[i]); 
            }
        }

        private void Update()
        {
            HandleSlotIndicator();
        }

        private void UpdateSlotIndicator(InventorySlotUI slotUI)
        {
            if(slotUI.AssignedInventorySlot.IsEmptySlot()) return;
            if(!MouseItemData.Instance.AssignedInventorySlot.IsEmptySlot()) return;
            foreach (var slot in slots)
            {
                if (slot != slotUI)
                {
                    if (slot.IsActiveIndicator())
                    {
                        slot.SetIndicator();
                    }
                }
            }
            slotUI.SetIndicator();
            OnSlotIndicatorChanged?.Invoke(slotUI);
        }

        private void HandleSlotIndicator()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UpdateSlotIndicator(slots[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UpdateSlotIndicator(slots[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UpdateSlotIndicator(slots[2]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                UpdateSlotIndicator(slots[3]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                UpdateSlotIndicator(slots[4]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                UpdateSlotIndicator(slots[5]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                UpdateSlotIndicator(slots[6]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                UpdateSlotIndicator(slots[7]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                UpdateSlotIndicator(slots[8]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                UpdateSlotIndicator(slots[9]);
            }
        }
    }
}