using UnityEngine;
using UnityEngine.Events;

using InventorySystem.Interfaces;

namespace InventorySystem
{
    public class ChestInventory : InventoryHolder, IInteractable
    {
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }
        
        public void Interact(Interactor interactor, out bool interactSuccessful)
        {
            OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem);
            interactSuccessful = true;
        }

        public void EndInteraction()
        {
            
        }
    }
}