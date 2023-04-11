using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private KeyCode interactKey = KeyCode.Space;
        private List<IInteractable> currentInteractorsInRange = new();
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out var interactable)) return;
            if(!currentInteractorsInRange.Contains(interactable))
                currentInteractorsInRange.Add(interactable);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(interactKey) || currentInteractorsInRange.Count == 0) return;
            currentInteractorsInRange[0].Interact(this);
        }
    }
}