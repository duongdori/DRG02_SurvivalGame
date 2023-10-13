using System.Collections.Generic;
using UnityEngine;

namespace CharacterCustomization
{
    public class CustomizationColorElement : MonoBehaviour
    {
        [SerializeField] private int index = 0;
        [SerializeField] private Material material;
        [SerializeField] private string colorName;
        [SerializeField] private List<Color> colors;
        
        private void Start()
        {
            ChangeColor();
        }

        public void PreviousColor()
        {
            index -= 1;
            if (index < 0)
            {
                index = colors.Count - 1;
            }
            
            ChangeColor();
        }
        
        public void NextColor()
        {
            index += 1;
            if (index > colors.Count - 1)
            {
                index = 0;
            }
            ChangeColor();
        }

        private void ChangeColor()
        {
            if (colors.Count > 0)
            {
                material.SetColor(colorName, colors[index]);
            }
            else
            {
                Debug.LogWarning("No " + colorName + " Specified In The Inspector");
            }
        }

        public Color GetCurrentColor()
        {
            return colors[index];
        }
    }
}