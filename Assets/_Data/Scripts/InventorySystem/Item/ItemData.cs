using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "SO/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public GameObject itemVisual;
    public ItemType itemType;
    public ToolType toolType;
    public ToolType toolNeeded;
    public HandSlot handSlot;
    public int damageAmount;
    public int maxStackSize = 1;
    public bool isStackable = false;
}
