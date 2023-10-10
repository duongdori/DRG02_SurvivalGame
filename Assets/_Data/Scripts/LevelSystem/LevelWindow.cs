using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.LevelSystem
{
    public class LevelWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Slider experienceBar;

        private LevelSystem _levelSystem;
        private LevelSystemAnimated _levelSystemAnimated;
        private void Awake()
        {
            levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            experienceBar = transform.Find("ExperienceBar").GetComponent<Slider>();
        }

        private void SetExperienceBarSize(float experienceNormalize)
        {
            experienceBar.value = experienceNormalize;
        }

        private void SetLevelNumber(int levelNumber)
        {
            levelText.SetText("Level: " + (levelNumber + 1).ToString());
        }

        public void SetLevelSystem(LevelSystem levelSystem)
        {
            _levelSystem = levelSystem;
        }

        public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
        {
            _levelSystemAnimated = levelSystemAnimated;
            
            SetLevelNumber(levelSystemAnimated.GetLevelNumber());
            SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

            levelSystemAnimated.OnLevelChanged += SetLevelNumber;
            levelSystemAnimated.OnExperienceChanged += SetExperienceBarSize;
        }
    }
}
