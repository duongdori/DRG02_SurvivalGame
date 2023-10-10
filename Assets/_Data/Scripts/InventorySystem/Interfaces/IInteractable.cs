using UnityEngine.Events;

namespace InventorySystem.Interfaces
{
    public interface IInteractable
    {
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }

        public void Interact(Interactor interactor, out bool interactSuccessful);

        public void EndInteraction();
    }
}