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
        private Inventory invent;

        private void Awake() => invent = GetComponent<Inventory>();
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out var interactable)) return;
            if(!currentInteractorsInRange.Contains(interactable))
                currentInteractorsInRange.Add(interactable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out var interactable)) return;
            if(currentInteractorsInRange.Contains(interactable))
                currentInteractorsInRange.Remove(interactable);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(interactKey)) return;
            for (var i = currentInteractorsInRange.Count - 1; i >= 0; i--)
            {
                if (currentInteractorsInRange[i].Equals(null)) currentInteractorsInRange.RemoveAt(i);
            }
            if (currentInteractorsInRange.Count == 0)
           invent.AttemptDropItem();
            else
            {
                var closestAngle = float.MaxValue;
                IInteractable closest = null;
                foreach (var interactable in currentInteractorsInRange)
                {
                    if (interactable.Equals(null)) continue;
                    var dir = interactable.GetTransform().position - transform.position;
                    var angle = Mathf.Abs(Vector3.Angle(transform.forward, dir));
                    Debug.Log($"Angle to {interactable.GetTransform().gameObject.name} is {angle}");
                    if (angle > closestAngle) continue;
                    closestAngle = angle;
                    closest = interactable;
                    Debug.Log($"New closest is {interactable.GetTransform().gameObject.name}");
                }

                if (closest != null)
                    closest.Interact(this);
            }
        }

        
    }
}