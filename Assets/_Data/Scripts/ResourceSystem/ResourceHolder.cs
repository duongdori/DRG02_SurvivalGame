using System;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;


public class ResourceHolder : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private ResourceData resourceData;
    public ResourceData ResourceData => resourceData;
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            DropResourceOnGround();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void DropResourceOnGround()
    {
        Vector3 pos = transform.position + Vector3.left * 2;
        GameObject newItem = Instantiate(itemPrefab, pos, Quaternion.identity);
        newItem.TryGetComponent(out ItemPickUp item);
        item.UpdateItem(resourceData.itemData, resourceData.amount);
    }
}
