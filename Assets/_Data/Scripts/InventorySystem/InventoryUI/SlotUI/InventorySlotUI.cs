using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image itemSprite;
        [SerializeField] private TextMeshProUGUI itemCount;
        [SerializeField] private Image indicator;
        
        [SerializeField] private InventorySlot assignedInventorySlot;
        public InventorySlot AssignedInventorySlot => assignedInventorySlot;
        
        public InventoryDisplay ParentDisplay { get; private set; }

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();

            itemSprite = transform.Find("ItemIcon").GetComponent<Image>();
            itemCount = GetComponentInChildren<TextMeshProUGUI>();
            indicator = transform.Find("Indicator").GetComponent<Image>();
            
            ClearSlot();
            
            _button.onClick.AddListener(OnUISlotClick);
        }

        public void Init(InventorySlot slot)
        {
            assignedInventorySlot = slot;
            UpdateUISlot(slot);
        }

        public void UpdateUISlot(InventorySlot slot)
        {
            if (slot.ItemData != null)
            {
                itemSprite.sprite = slot.ItemData.itemIcon;
                itemSprite.color = Color.white;

                itemCount.text = slot.StackSize > 1 ? slot.StackSize.ToString() : "";
            }
            else
            {
                ClearSlot();
            }
        }

        public void UpdateUISlot()
        {
            if (assignedInventorySlot != null)
            {
                UpdateUISlot(assignedInventorySlot);
            }
        }
        public void ClearSlot()
        {
            assignedInventorySlot?.ClearSlot();
            itemSprite.sprite = null;
            itemSprite.color = Color.clear;
            itemCount.text = "";
            indicator.enabled = false;
        }

        private void OnUISlotClick()
        {
            ParentDisplay.SlotClicked(this);
        }

        public void SetIndicator()
        {
            indicator.enabled = !indicator.IsActive();
        }

        public bool IsActiveIndicator()
        {
            return indicator.IsActive();
        }
    }
}

