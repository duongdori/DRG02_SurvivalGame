using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    public class DynamicInventoryDisplay : InventoryDisplay
    {
        [SerializeField] protected InventorySlotUI slotPrefab;
        protected override void Start()
        {
            base.Start();
        }

        public override void AssignSlot(InventorySystem invToDisplay)
        {
            ClearSlots();

            slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();
            
            if(invToDisplay == null) return;

            for (int i = 0; i < invToDisplay.InventorySize; i++)
            {
                InventorySlotUI uiSlot = Instantiate(slotPrefab, transform);
                slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
                uiSlot.Init(invToDisplay.InventorySlots[i]);
                uiSlot.UpdateUISlot();
            }
        }

        public void RefreshDynamicInventory(InventorySystem invToDisplay)
        {
            ClearSlots();
            inventorySystem = invToDisplay;
            if (inventorySystem != null)
            {
                inventorySystem.OnInventorySlotChanged += UpdateSlot;
            }
            AssignSlot(invToDisplay);
        }

        private void ClearSlots()
        {
            foreach (var item in transform.Cast<Transform>())
            {
                Destroy(item.gameObject);
            }

            slotDictionary?.Clear();
        }

        private void OnDisable()
        {
            if (inventorySystem != null)
            {
                inventorySystem.OnInventorySlotChanged -= UpdateSlot;
            }
        }
    }
}