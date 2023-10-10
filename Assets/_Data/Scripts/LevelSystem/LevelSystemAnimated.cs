using System;
using UnityEngine;

namespace Scripts.LevelSystem
{
    public class LevelSystemAnimated
    {
        public Action<float> OnExperienceChanged;
        public Action<int> OnLevelChanged;
        
        private LevelSystem _levelSystem;
        private bool isAnimating;
        private float updateTimer;
        private float updateTimerMax;
        
        private int level;
        private int experience;

        public LevelSystemAnimated(LevelSystem levelSystem)
        {
            SetLevelSystem(levelSystem);
            updateTimerMax = 0.016f;
        }
        private void SetLevelSystem(LevelSystem levelSystem)
        {
            _levelSystem = levelSystem;

            level = levelSystem.GetLevelNumber();
            experience = levelSystem.GetExperience();
            
            _levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
            _levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        }

        private void LevelSystem_OnLevelChanged(int obj)
        {
            isAnimating = true;
        }

        private void LevelSystem_OnExperienceChanged(float obj)
        {
            isAnimating = true;
        }

        public void Update()
        {
            if (isAnimating)
            {
                updateTimer += Time.deltaTime;
                while (updateTimer > updateTimerMax)
                {
                    updateTimer -= updateTimerMax;
                
                    UpdateAddExperience();
                }
            }
            
            // Debug.Log(level + " / " + experience);
        }

        private void UpdateAddExperience()
        {
            if (level < _levelSystem.GetLevelNumber())
            {
                //Local level under target level
                AddExperience();
            }
            else
            {
                //Local level equals target level
                if (experience < _levelSystem.GetExperience())
                {
                    AddExperience();
                }
                else
                {
                    isAnimating = false;
                }
            }
        }

        private void AddExperience()
        {
            experience++;
            if (experience >= _levelSystem.GetExperienceToNextLevel(level))
            {
                level++;
                experience = 0;
                OnLevelChanged?.Invoke(level);
            }
            
            OnExperienceChanged?.Invoke(GetExperienceNormalized());
        }
        
        public int GetLevelNumber()
        {
            return level;
        }
        
        public float GetExperienceNormalized()
        {
            if (_levelSystem.IsMaxLevel(level))
            {
                return 1f;
            }
            
            return (float)experience / _levelSystem.GetExperienceToNextLevel(level);
        }
    }
    
}
