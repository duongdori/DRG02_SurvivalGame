using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    public class MouseItemData : MonoBehaviour
    {
        public static MouseItemData Instance { get; private set; }
        
        [SerializeField] private Image itemSprite;
        [SerializeField] private TextMeshProUGUI itemCount;
        
        [SerializeField] private InventorySlot assignedInventorySlot;
        public InventorySlot AssignedInventorySlot => assignedInventorySlot;

        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private float dropOffset = 3f;
        private Transform _playerTransform;
        private bool isIndicator = false;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _playerTransform = GameObject.Find("Player").transform;
            if(_playerTransform == null) Debug.LogWarning("Player not found!", gameObject);
            
            itemSprite = GetComponentInChildren<Image>();
            itemCount = GetComponentInChildren<TextMeshProUGUI>();
            
            ClearSlot();
        }

        private void Update()
        {
            if (!assignedInventorySlot.IsEmptySlot())
            {
                transform.position = Mouse.current.position.ReadValue();

                if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
                {
                    Vector3 pos = _playerTransform.position + _playerTransform.forward * dropOffset;
                    GameObject item = Instantiate(itemPrefab, pos, Quaternion.identity);
                    
                    ItemPickUp itemPickUp = item.GetComponent<ItemPickUp>();
                    if (itemPickUp != null)
                    {
                        itemPickUp.UpdateItem(assignedInventorySlot.ItemData, assignedInventorySlot.StackSize);
                    }
                    
                    ClearSlot();
                }
            }
        }

        public void ClearSlot()
        {
            assignedInventorySlot.ClearSlot();
            itemCount.text = "";
            itemSprite.color = Color.clear;
            itemSprite.sprite = null;
            isIndicator = false;
        }

        private static bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        public void UpdateMouseSlot(InventorySlot invSlot, bool indicator)
        {
            assignedInventorySlot.AssignItem(invSlot);
            itemSprite.sprite = invSlot.ItemData.itemIcon;
            itemSprite.color = Color.white;
            itemCount.text = invSlot.StackSize.ToString();
            isIndicator = indicator;
        }

        public bool GetIsIndicator()
        {
            return isIndicator;
        }
    }
}
