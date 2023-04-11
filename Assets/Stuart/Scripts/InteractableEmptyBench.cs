using System;
using UnityEngine;

namespace Stuart
{
    public class InteractableEmptyBench : Bench
    {
        private void OnValidate()
        {
            if(itemSpot==null)
                Debug.LogWarning("Missing itemspot");
        }

        public override void Interact(Interactor interactor)
        {
            Debug.Log("Interacted with bench ");
            if (!interactor.TryGetComponent<Inventory>(out var invent)) return;
            if (invent.CurrentItem != null)
            {
                if (CurrentItem != null)
                {
                    var cached = CurrentItem;
                    AddItemToBench(invent);
                    AddItemToPlayerInvent(invent,cached);
                }
                else
                    AddItemToBench(invent);
            }
            else
            {
                AddItemToPlayerInvent(invent,CurrentItem);
                RemoveItemFromBench(invent);
            }
        }

        
    }
}