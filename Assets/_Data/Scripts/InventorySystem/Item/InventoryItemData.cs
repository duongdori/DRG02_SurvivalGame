using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "SO/Item Data", fileName = "New Item Data")]
    public class InventoryItemData : ScriptableObject
    {
        public int itemID;
        public string itemName;
        public Sprite itemIcon;
        public GameObject itemPrefab;
        public GameObject itemVisual;
        public int maxStackSize;
        public ItemType itemType;
        public HandSlot handSlot;
        public int damageAmount;
        public ToolType toolType;
        public ToolType toolNeeded;

    }
}