using UnityEngine;

namespace StaminaSystem
{
    public class StaminaSystem
    {
        public int staminaMax = 100;
        private float staminaAmount;
        private float staminaRegenAmount;

        public StaminaSystem()
        {
            staminaAmount = 0f;
            staminaRegenAmount = 30f;
        }

        public void Update()
        {
            staminaAmount += staminaRegenAmount * Time.deltaTime;
            staminaAmount = Mathf.Clamp(staminaAmount, 0f, staminaMax);
        }

        public void TrySpendMana(int amount)
        {
            if (staminaAmount >= amount)
            {
                staminaAmount -= amount;
            }
        }

        public float GetStaminaNormalized()
        {
            return staminaAmount / staminaMax;
        }

        public int GetStaminaAmount()
        {
            return (int)staminaAmount;
        }
    }
    
}