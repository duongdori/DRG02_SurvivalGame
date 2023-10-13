using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterCustomization
{
    public class CustomizationPickerUI : MyMonoBehaviour
    {
        [SerializeField] private CustomizationElement customizationElement;
        [SerializeField] private TMP_Text elementName;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private TMP_Text elementID;

        protected override void Start()
        {
            base.Start();
            
            UpdateElementID(customizationElement.ElementID);
            previousButton.onClick.AddListener((() =>
            {
                customizationElement.PreviousElement();
                UpdateElementID(customizationElement.ElementID);
            }));
            nextButton.onClick.AddListener((() =>
            {
                customizationElement.NextElement();
                UpdateElementID(customizationElement.ElementID);
            }));
        }

        public void Randomize()
        {
            customizationElement.Randomize();
            UpdateElementID(customizationElement.ElementID);
        }

        private void UpdateElementID(int value)
        {
            elementID.SetText(value.ToString());
        }

        public void UpdateElementName(string newName)
        {
            elementName.SetText(newName);
        }

        public void SetCustomizationElement(CustomizationElement element)
        {
            customizationElement = element;
        }
        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadElementName();
            LoadPreviousButton();
            LoadNextButton();
            LoadElementID();
        }

        private void LoadElementName()
        {
            if(elementName != null) return;
            elementName = transform.Find("ElementName").GetComponent<TMP_Text>();
            Debug.LogWarning(transform.name + ": LoadElementName", gameObject);
        }
        private void LoadPreviousButton()
        {
            if(previousButton != null) return;
            previousButton = transform.Find("PickerButton").GetChild(0).GetComponent<Button>();
            Debug.LogWarning(transform.name + ": LoadPreviousButton", gameObject);
        }
        private void LoadNextButton()
        {
            if(nextButton != null) return;
            nextButton = transform.Find("PickerButton").GetChild(1).GetComponent<Button>();
            Debug.LogWarning(transform.name + ": LoadNextButton", gameObject);
        }
        private void LoadElementID()
        {
            if(elementID != null) return;
            elementID = transform.Find("PickerButton").GetChild(2).GetComponent<TMP_Text>();
            Debug.LogWarning(transform.name + ": LoadElementID", gameObject);
        }
    }
}
