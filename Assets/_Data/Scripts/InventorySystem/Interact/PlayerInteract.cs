using UnityEngine;

namespace InventorySystem
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryHolder playerInventoryHolder;

        private void Awake()
        {
            playerInventoryHolder = GetComponent<PlayerInventoryHolder>();
        }

        private void OnTriggerEnter(Collider other)
        {
            ItemPickUp item = other.gameObject.GetComponent<ItemPickUp>();
            
            if(item == null) return;

            if (playerInventoryHolder.AddToInventory(item.ItemData, item.StackSize))
            {
                item.DestroySelf();
            }
            else
            {
                Debug.Log("Fulllllll");
            }
            
        }
    }
}

