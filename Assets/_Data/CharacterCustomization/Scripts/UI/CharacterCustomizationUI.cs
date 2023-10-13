using UnityEngine;
using UnityEngine.UI;

namespace CharacterCustomization
{
    public class CharacterCustomizationUI : MonoBehaviour
    {
        [SerializeField] private CharacterCustomization characterCustomization;
        
        [SerializeField] private GameObject selectGenderButton;
        [SerializeField] private GameObject characterCustomizerWindow;

        [SerializeField] private Button selectMaleButton;
        [SerializeField] private Button selectFemaleButton;
        
        [SerializeField] private Button randomizeButton;

        private void Awake()
        {
            characterCustomizerWindow.SetActive(false);

            selectMaleButton.onClick.AddListener((() => SelectGender(Gender.Male)));
            selectFemaleButton.onClick.AddListener((() => SelectGender(Gender.Female)));
        }

        private void SelectGender(Gender gender)
        {
            selectGenderButton.SetActive(false);
            characterCustomizerWindow.SetActive(true);
            characterCustomization.SetGender(gender);
            characterCustomization.InitBaseCharacter();
            characterCustomization.UpdateElements();
        }
    }
}
