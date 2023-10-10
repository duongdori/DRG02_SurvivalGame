using System;
using System.Collections;
using System.Collections.Generic;
using DamageSystem.Popup;
using InventorySystem;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private Collider _collider;
    public InventoryItemData itemData;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        DisableDamageCollider();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Resource"))
        {
            ResourceHolder resource = collision.gameObject.GetComponent<ResourceHolder>();
            if(resource == null) return;
            
            Vector3 closestPoint = collision.ClosestPoint(transform.position);

            if (itemData.toolType != resource.ResourceData.itemData.toolNeeded)
            {
                DamagePopup.Create(PlayerManager.Instance.damagePopupPrefab.transform, closestPoint,
                    0);
            }
            else
            {
                Debug.Log("Damage " + collision.gameObject.name);
                resource.TakeDamage(PlayerManager.Instance.playerStats.GetCurrentDamage());
            
                DamagePopup.Create(PlayerManager.Instance.damagePopupPrefab.transform, closestPoint,
                    PlayerManager.Instance.playerStats.GetCurrentDamage());
            }
        }
    }

    public void EnableDamageCollider()
    {
        _collider.enabled = true;
    }
    
    public void DisableDamageCollider()
    {
        _collider.enabled = false;
    }
}
