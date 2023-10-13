using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterCustomization
{
    public class CustomizationGroupPickerUI : MyMonoBehaviour
    {
        [SerializeField] private List<CustomizationElement> customizationElements;
        [SerializeField] private TMP_Text elementName;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private TMP_Text elementID;

        protected override void Start()
        {
            base.Start();
            
            UpdateElementID();
            previousButton.onClick.AddListener((() =>
            {
                foreach (var element in customizationElements)
                {
                    element.PreviousElement();
                }
                UpdateElementID();
            }));
            nextButton.onClick.AddListener((() =>
            {
                foreach (var element in customizationElements)
                {
                    element.NextElement();
                }
                UpdateElementID();
            }));
        }

        public void Randomize()
        {
            int id = Random.Range(0, customizationElements[0].Elements.Count);
            foreach (var element in customizationElements)
            {
                element.Randomize(id);
            }
            
            UpdateElementID();
        }
        private void UpdateElementID()
        {
            elementID.SetText(customizationElements[0].ElementID.ToString());
        }

        public void UpdateElementName(string newName)
        {
            elementName.SetText(newName);
        }

        public void SetCustomizationElements(CustomizationElement element)
        {
            customizationElements.Add(element);
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