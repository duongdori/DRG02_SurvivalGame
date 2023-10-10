using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.HealthSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider damageBarImage;

        private float damageHealthShrinkTimerMax = 1f;
        private float damageHealthShrinkTimer;
        private HealthSystem _healthSystem;

        private void Awake()
        {
            healthText = transform.Find("HealthText").GetComponent<TextMeshProUGUI>();
            healthBar = transform.Find("HealthBar").GetComponent<Slider>();
            damageBarImage = healthBar.transform.Find("DamageBarImage").GetComponent<Slider>();
        }

        public void Setup(HealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
            SetHealthNumber(_healthSystem.GetHealth());
            SetHealthBarSize(_healthSystem.GetHealthPercent());

            damageBarImage.value = healthBar.value;
            
            _healthSystem.OnHealthChanged += SetHealthNumber;
            _healthSystem.OnHealthPercentChanged += SetHealthBarSize;
            _healthSystem.OnDamaged += HealthSystem_OnDamaged;
            _healthSystem.OnHealed += HealthSystem_OnHealed;
        }

        private void HealthSystem_OnDamaged()
        {
            damageHealthShrinkTimer = damageHealthShrinkTimerMax;
        }
        private void HealthSystem_OnHealed()
        {
            damageBarImage.value = _healthSystem.GetHealthPercent();
        }

        private void Update()
        {
            damageHealthShrinkTimer -= Time.deltaTime;
            if (damageHealthShrinkTimer < 0)
            {
                if (healthBar.value < damageBarImage.value)
                {
                    float shrinkSpeed = 1f;
                    damageBarImage.value -= shrinkSpeed * Time.deltaTime;
                }
            }
        }

        private void SetHealthBarSize(float healthPercent)
        {
            healthBar.value = healthPercent;
        }

        private void SetHealthNumber(int healthNumber)
        {
            healthText.SetText("Health: " + healthNumber.ToString());
        }
    }
}
