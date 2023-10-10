using System;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.Serialization;

using Scripts.Player.Manager;

public class WeaponSlotManager : MonoBehaviour
{
    [SerializeField] private Transform rightHandSlot;
    [SerializeField] private Transform leftHandSlot;

    [SerializeField] private InventoryItemData itemOnRightHand;
    [SerializeField] private InventoryItemData itemOnLeftHand;

    public DamageCollider weaponDamageCollider;

    private void OnEnable()
    {
        StaticInventoryDisplay.OnSlotIndicatorChanged += InitializeItemOnHand;
    }
    
    private void OnDisable()
    {
        StaticInventoryDisplay.OnSlotIndicatorChanged -= InitializeItemOnHand;
    }
    
    private void ClearRightHandSlot()
    {
        if(itemOnRightHand == null && rightHandSlot.childCount <= 0) return;
        
        for (int i = 0; i < rightHandSlot.childCount; i++)
        {
            Destroy(rightHandSlot.GetChild(i).gameObject);
        }
    
        itemOnRightHand = null;
        PlayerManager.Instance.playerStats.SetCurrentDamage(0);
    }
    
    private void ClearLeftHandSlot()
    {
        if(itemOnLeftHand == null && leftHandSlot.childCount <= 0) return;
        
        for (int i = 0; i < leftHandSlot.childCount; i++)
        {
            Destroy(leftHandSlot.GetChild(i).gameObject);
        }
    
        itemOnLeftHand = null;
    }
    
    private void InitializeItemOnHand(InventorySlotUI inventorySlotUI)
    {
        if (inventorySlotUI.AssignedInventorySlot.IsEmptySlot())
        {
            ClearRightHandSlot();
            ClearLeftHandSlot();
            return;
        }
    
        if (inventorySlotUI.AssignedInventorySlot.ItemData.handSlot == HandSlot.RightHand)
        {
            ClearRightHandSlot();
            itemOnRightHand = InitializeItemOnHand(inventorySlotUI, rightHandSlot);
            
            if(itemOnRightHand == null) return;
            
            weaponDamageCollider = rightHandSlot.GetChild(0).GetComponent<DamageCollider>();
            weaponDamageCollider.itemData = itemOnRightHand;
            PlayerManager.Instance.playerStats.SetCurrentDamage(itemOnRightHand.damageAmount);
        }
        else if (inventorySlotUI.AssignedInventorySlot.ItemData.handSlot == HandSlot.LeftHand)
        {
            ClearLeftHandSlot();
            itemOnLeftHand = InitializeItemOnHand(inventorySlotUI, leftHandSlot);
        }
        else
        {
            ClearRightHandSlot();
            ClearLeftHandSlot();
        }
        
    }
    
    private InventoryItemData InitializeItemOnHand(InventorySlotUI inventorySlotUI, Transform handSlot)
    {
        if(!inventorySlotUI.IsActiveIndicator()) return null;
        
        InventoryItemData itemData = inventorySlotUI.AssignedInventorySlot.ItemData;
        GameObject item = Instantiate(itemData.itemPrefab, handSlot);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    
        return itemData;
    }

    public bool HasItemOnRightHand()
    {
        return itemOnRightHand != null;
    }
    
    public bool HasItemOnLeftHand()
    {
        return itemOnLeftHand != null;
    }

    public InventoryItemData GetItemOnRightHand()
    {
        return itemOnRightHand;
    }
}
