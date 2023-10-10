using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{
    [Serializable]
    public class InventorySystem
    {
        [SerializeField] private List<InventorySlot> inventorySlots;
        public List<InventorySlot> InventorySlots => inventorySlots;

        public int InventorySize => inventorySlots.Count;

        public UnityAction<InventorySlot> OnInventorySlotChanged;
        public UnityAction<InventoryItemData, int> OnInventoryItemRemaining;

        public InventorySystem(int size)
        {
            inventorySlots = new List<InventorySlot>(size);

            for (int i = 0; i < size; i++)
            {
                inventorySlots.Add(new InventorySlot());
            }
        }
        
        public bool CanAddItem(InventoryItemData itemData)
        {
            InventorySlot slotWithStackableItem = FindSlot(itemData);
            return HasFreeSlot(out InventorySlot freeSlot) || slotWithStackableItem != null;
        }
        private InventorySlot FindSlot(InventoryItemData itemData)
        {
            return inventorySlots.FirstOrDefault(slot => slot.ItemData == itemData && slot.CanAddAmountToStackSize());
        }
        public bool HasFreeSlot(out InventorySlot freeSlot)
        {
            freeSlot = inventorySlots.FirstOrDefault(slot => slot.IsEmptySlot());

            return freeSlot != null;
        }
        
        public void AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
        {
            InventorySlot slotContainsItem = FindSlot(itemToAdd);

            if (slotContainsItem != null)
            {
                AddToSlotContainsItem(slotContainsItem, itemToAdd, amountToAdd);
            }
            else
            {
                AddItemToEmptySlot(itemToAdd, amountToAdd);
            }
        }

        private void AddToSlotContainsItem(InventorySlot slot, InventoryItemData itemToAdd, int amountToAdd)
        {
            if (slot.EnoughRoomLeftInStack(amountToAdd, out int remaining))
            {
                slot.AddToStack(amountToAdd);
                OnInventorySlotChanged?.Invoke(slot);
            }
            else
            {
                slot.AddToStack(remaining);
                OnInventorySlotChanged?.Invoke(slot);
                amountToAdd -= remaining;
                AddToInventory(itemToAdd, amountToAdd);
            }
        }
        
        private void AddItemToEmptySlot(InventoryItemData itemToAdd, int amountToAdd)
        {
            if (HasFreeSlot(out InventorySlot freeSlot))
            {
                if (amountToAdd <= itemToAdd.maxStackSize)
                {
                    freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                    OnInventorySlotChanged?.Invoke(freeSlot);
                }
                else
                {
                    freeSlot.UpdateInventorySlot(itemToAdd, itemToAdd.maxStackSize);
                    OnInventorySlotChanged?.Invoke(freeSlot);
                    amountToAdd -= itemToAdd.maxStackSize;
                    AddToInventory(itemToAdd, amountToAdd);
                }
            }
            else
            {
                if (amountToAdd > 0)
                {
                    OnInventoryItemRemaining?.Invoke(itemToAdd, amountToAdd);
                    // TODO: Full slot - drop item on ground
                }
            }
        }
    }
}
