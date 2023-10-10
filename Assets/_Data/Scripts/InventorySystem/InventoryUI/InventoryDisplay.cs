using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InventorySystem
{
    public abstract class InventoryDisplay : MonoBehaviour
    {
        protected InventorySystem inventorySystem;
        public InventorySystem InventorySystem => inventorySystem;

        protected Dictionary<InventorySlotUI, InventorySlot> slotDictionary;
        public Dictionary<InventorySlotUI, InventorySlot> SlotDictionary => slotDictionary;

        protected virtual void Start()
        {
            
        }

        public abstract void AssignSlot(InventorySystem invToDisplay);

        protected virtual void UpdateSlot(InventorySlot updateSlot)
        {
            foreach (var slot in slotDictionary)
            {
                if (slot.Value == updateSlot)
                {
                    slot.Key.UpdateUISlot(updateSlot); 
                }
            }
        }

        public void SlotClicked(InventorySlotUI clickedUISlot)
        {
            InventoryItemData clickedSlotItemData = clickedUISlot.AssignedInventorySlot.ItemData;
            InventoryItemData mouseSlotItemData = MouseItemData.Instance.AssignedInventorySlot.ItemData;
            
            // Clicked slot has an item - Mouse doesn't have an item - pick up that item
            if (clickedSlotItemData != null && mouseSlotItemData == null)
            {
                PickUpItemToMouseSlot(clickedUISlot);
                return;
            }
            
            // Clicked slot doesn't have an item - Mouse does have an item - place the mouse item into the empty slot
            if (clickedSlotItemData == null && mouseSlotItemData != null)
            {
                PlaceMouseItemIntoEmptySlot(clickedUISlot);
                return;
            }
            
            // Clicked slot has an item - Mouse does have an item - swap slots and combine slots
            if (clickedSlotItemData != null && mouseSlotItemData != null)
            {
                if (clickedSlotItemData != mouseSlotItemData)
                {
                    SwapSlots(clickedUISlot);
                    return;
                }
                
                CombineSlots(clickedUISlot, mouseSlotItemData);
            }
        }

        private void PickUpItemToMouseSlot(InventorySlotUI clickedUISlot)
        {
            bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;

            // If player is holding shift key? Split the stack.
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                MouseItemData.Instance.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
            }
            else
            {
                MouseItemData.Instance.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
            }
        }
        private void PlaceMouseItemIntoEmptySlot(InventorySlotUI clickedUISlot)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(MouseItemData.Instance.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();
                
            MouseItemData.Instance.ClearSlot();
        }
        private void SwapSlots(InventorySlotUI clickedUISlot)
        {
            InventorySlot clonedSlot = new InventorySlot(MouseItemData.Instance.AssignedInventorySlot.ItemData,
                MouseItemData.Instance.AssignedInventorySlot.StackSize);
            
            MouseItemData.Instance.ClearSlot();
            MouseItemData.Instance.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            
            clickedUISlot.ClearSlot();
            clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
            clickedUISlot.UpdateUISlot();
        }
        private void CombineSlots(InventorySlotUI clickedUISlot, InventoryItemData mouseSlotItemData)
        {
            int mouseSlotStackSize = MouseItemData.Instance.AssignedInventorySlot.StackSize;
                    
            if (clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseSlotStackSize, out int leftInStack))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(MouseItemData.Instance.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();
                        
                MouseItemData.Instance.ClearSlot();
                return;
            }

            if (leftInStack < 1)
            {
                SwapSlots(clickedUISlot);
                return;
            }

            int remainingOnMouse = mouseSlotStackSize - leftInStack;
                            
            clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
            clickedUISlot.UpdateUISlot();

            InventorySlot newItem = new InventorySlot(mouseSlotItemData, remainingOnMouse);
            MouseItemData.Instance.ClearSlot();
            MouseItemData.Instance.UpdateMouseSlot(newItem);
        }
    }
}