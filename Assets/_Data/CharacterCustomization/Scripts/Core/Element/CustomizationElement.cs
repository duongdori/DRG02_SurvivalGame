using System.Collections.Generic;
using UnityEngine;

namespace CharacterCustomization
{
    public class CustomizationElement : MonoBehaviour
    {
        [SerializeField] private CharacterCustomization characterCustomization;
        [SerializeField] private int elementID = 0;
        [SerializeField] private List<GameObject> elements;
        public int ElementID => elementID;
        public List<GameObject> Elements => elements;

        public void PreviousElement()
        {
            if(elements.Count <= 0) return;
            
            elements[elementID].SetActive(false);
            characterCustomization.RemoveEnableObject(elements[elementID]);
            elementID -= 1;
            if (elementID < 0)
            {
                elementID = elements.Count - 1;
            }
            characterCustomization.ActivateItem(elements[elementID]);
        }
        
        public void NextElement()
        {
            if(elements.Count <= 0) return;

            elements[elementID].SetActive(false);
            characterCustomization.RemoveEnableObject(elements[elementID]);
            elementID += 1;
            if (elementID > elements.Count - 1)
            {
                elementID = 0;
            }
            characterCustomization.ActivateItem(elements[elementID]);
        }

        public void Randomize()
        {
            if(elements.Count <= 0) return;
            
            elements[elementID].SetActive(false);
            characterCustomization.RemoveEnableObject(elements[elementID]);
            elementID = Random.Range(0, elements.Count);
            characterCustomization.ActivateItem(elements[elementID]);
        }
        
        public void Randomize(int id)
        {
            if(elements.Count <= 0) return;
            
            elements[elementID].SetActive(false);
            characterCustomization.RemoveEnableObject(elements[elementID]);
            elementID = id;
            characterCustomization.ActivateItem(elements[elementID]);
        }

        public void SetElements(List<GameObject> value)
        {
            elements = value;
        }
        public void SetCharacterCustomizer(CharacterCustomization customization)
        {
            characterCustomization = customization;
        }
    }
}
