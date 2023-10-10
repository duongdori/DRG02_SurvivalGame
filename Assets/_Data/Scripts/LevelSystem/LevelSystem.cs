using System;
using UnityEngine;

namespace Scripts.LevelSystem
{
    public class LevelSystem
    {
        public Action<float> OnExperienceChanged;
        public Action<int> OnLevelChanged;

        private readonly int[] experiencePerLevel = new[] { 100, 150, 200, 250, 300, 350, 400, 450, 500 };
        
        private int _level;
        private int _experience;

        public LevelSystem()
        {
            _level = 0;
            _experience = 0;
        }

        public void AddExperience(int amount)
        {
            if(IsMaxLevel()) return;
            
            _experience += amount;

            while (!IsMaxLevel() && _experience >= GetExperienceToNextLevel(_level))
            {
                _experience -= GetExperienceToNextLevel(_level);
                _level++;
                OnLevelChanged?.Invoke(_level);
            }
            
            OnExperienceChanged?.Invoke(GetExperienceNormalized());
        }

        public float GetExperienceNormalized()
        {
            if (IsMaxLevel())
            {
                return 1f;
            }
            
            return (float)_experience / GetExperienceToNextLevel(_level);
        }

        public int GetExperience()
        {
            return _experience;
        }

        public int GetExperienceToNextLevel(int level)
        {
            if (level < experiencePerLevel.Length)
            {
                return experiencePerLevel[level];
            }

            Debug.LogError("Level invalid: " + level);
            return 100;
        }
        public int GetLevelNumber()
        {
            return _level;
        }

        private bool IsMaxLevel()
        {
            return IsMaxLevel(_level);
        }
        public bool IsMaxLevel(int level)
        {
            return level == experiencePerLevel.Length - 1;
        }
    }
}
