using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player.Stats
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 50;
        [SerializeField] private int currentHealth;
        [SerializeField] private int currentDamage;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
        
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Player Die!");
        }

        public void SetCurrentDamage(int newDamage)
        {
            currentDamage = newDamage;
        }

        public int GetCurrentDamage()
        {
            return currentDamage;
        }
    }
}

