
using System;

namespace Scripts.HealthSystem
{
    public class HealthSystem
    {
        public Action<float> OnHealthPercentChanged;
        public Action<int> OnHealthChanged;
        public Action OnDamaged;
        public Action OnHealed;
        
        private int _healthMax;
        private int _health;

        public HealthSystem(int healthMax)
        {
            _healthMax = healthMax;
            _health = _healthMax;
        }

        public int GetHealth()
        {
            return _health;
        }

        public float GetHealthPercent()
        {
            return (float)_health / _healthMax;
        }

        public void Damage(int damageAmount)
        {
            _health -= damageAmount;
            if (_health < 0)
            {
                _health = 0;
            }
            
            OnDamaged?.Invoke();
            OnHealthChanged?.Invoke(_health);
            OnHealthPercentChanged?.Invoke(GetHealthPercent());
        }

        public void Heal(int healAmount)
        {
            _health += healAmount;
            if (_health > _healthMax)
            {
                _health = _healthMax;
            }
            
            OnHealed?.Invoke();
            OnHealthChanged?.Invoke(_health);
            OnHealthPercentChanged?.Invoke(GetHealthPercent());
        }
    }
}
