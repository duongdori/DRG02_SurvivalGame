using System;
using InventorySystem.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InventorySystem
{
    public class Interactor : MonoBehaviour
    {
        public Transform interactionPoint;
        public LayerMask interactionLayer;
        public float interactionPointRadius = 1f;
        public bool isInteracting;

        private void Update()
        {
            var colliders = Physics.OverlapSphere(interactionPoint.position, interactionPointRadius,
                interactionLayer);

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    var interactable = colliders[i].GetComponent<IInteractable>();

                    if (interactable != null)
                    {
                        StartInteraction(interactable);
                    }
                }
            }
        }

        private void StartInteraction(IInteractable interactable)
        {
            interactable.Interact(this, out bool interactSuccessful);
            isInteracting = true;
        }

        private void EndInteraction()
        {
            isInteracting = false;
        }
    }
}