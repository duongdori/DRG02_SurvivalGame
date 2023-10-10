using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace InventorySystem
{
    [Serializable]
    public class InventorySlot
    {
        [SerializeField] private InventoryItemData itemData;
        public InventoryItemData ItemData => itemData;
        
        [SerializeField] private int stackSize;
        public int StackSize => stackSize;
        
        public InventorySlot(InventoryItemData itemData, int stackSize)
        {
            this.itemData = itemData;
            this.stackSize = stackSize;
        }

        public InventorySlot()
        {
            ClearSlot();
        }
        
        public void ClearSlot()
        {
            itemData = null;
            stackSize = -1;
        }
        
        public void AssignItem(InventorySlot invSlot)
        {
            if (itemData == invSlot.ItemData)
            {
                AddToStack(invSlot.stackSize);
            }
            else
            {
                itemData = invSlot.ItemData;
                stackSize = 0;
                AddToStack(invSlot.stackSize);
            }
        }
        
        public void UpdateInventorySlot(InventoryItemData data, int amount)
        {
            itemData = data;
            stackSize = amount;
        }

        public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining)
        {
            amountRemaining = itemData.maxStackSize - stackSize;

            return EnoughRoomLeftInStack(amountToAdd);
        }

        public bool EnoughRoomLeftInStack(int amountToAdd)
        {
            return stackSize + amountToAdd <= itemData.maxStackSize;
        }
        
        public bool CanAddAmountToStackSize()
        {
            return stackSize < itemData.maxStackSize;
        }
        
        public void AddToStack(int amount)
        {
            stackSize += amount;
        }

        public void RemoveFromStack(int amount)
        {
            stackSize -= amount;
        }

        public bool SplitStack(out InventorySlot splitStack)
        {
            if (stackSize <= 1)
            {
                splitStack = null;
                return false;
            }

            int halfStack = Mathf.RoundToInt((float)stackSize / 2);
            RemoveFromStack(halfStack);

            splitStack = new InventorySlot(itemData, halfStack);
            return true;
        }
        
        public bool IsEmptySlot()
        {
            return itemData == null;
        }
    }
}
