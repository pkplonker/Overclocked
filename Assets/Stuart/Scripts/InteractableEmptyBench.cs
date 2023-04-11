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
            Debug.Log($"Interacted with bench ");
            if (!interactor.TryGetComponent<Inventory>(out var invent)) return;
            if (invent.CurrentItem != null)
                AddItemToBench(invent);
            else
            {
                AddItemToPlayerInvent(invent);
                RemoveItemFromBench(invent);
            }
        }

        private void RemoveItemFromBench(Inventory invent)
        {
            if (invent == null) return;
            if (invent.CurrentItem != null)
                invent.RemoveItem();
            invent.AddItem(currentItem);
            Destroy(currentSpawnedItem);
            currentItem = null;
        }
    }
}