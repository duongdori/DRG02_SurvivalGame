using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StaminaSystem
{
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI staminaText;
        [SerializeField] private Slider staminaBar;
        [SerializeField] private Button spendStaminaButton;
        private StaminaSystem staminaSystem;
        private void Awake()
        {
            staminaText = transform.Find("StaminaText").GetComponent<TextMeshProUGUI>();
            staminaBar = transform.Find("StaminaBar").GetComponent<Slider>();
            spendStaminaButton = transform.Find("SpendStaminaButton").GetComponent<Button>();
            staminaSystem = new StaminaSystem();

            spendStaminaButton.onClick.AddListener((() =>
            {
                staminaSystem.TrySpendMana(30);
            }));
        }

        private void Update()
        {
            staminaSystem.Update();

            staminaBar.value = staminaSystem.GetStaminaNormalized();
            staminaText.SetText("Stamina: " + staminaSystem.GetStaminaAmount());
        }
    }
}
