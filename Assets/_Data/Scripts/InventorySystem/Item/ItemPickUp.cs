using System;
using UnityEngine;

namespace InventorySystem
{
    [RequireComponent(typeof(SphereCollider))]
    public class ItemPickUp : MonoBehaviour
    {
        [SerializeField] private float pickUpRadius = 1f;

        [SerializeField] private InventoryItemData itemData;
        public InventoryItemData ItemData => itemData;

        [SerializeField] private int stackSize;
        public int StackSize => stackSize;

        private SphereCollider _myCollider;

        private void Awake()
        {
            _myCollider = GetComponent<SphereCollider>();
            _myCollider.isTrigger = true;
            _myCollider.radius = pickUpRadius;
        }

        public void UpdateItem(InventoryItemData itemData, int amount)
        {
            this.itemData = itemData;
            stackSize = amount;
            transform.name = this.itemData.itemName;
            CreateItemVisual();
        }

        private void CreateItemVisual()
        {
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }

            GameObject item = Instantiate(itemData.itemVisual, Vector3.zero, Quaternion.identity, transform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
