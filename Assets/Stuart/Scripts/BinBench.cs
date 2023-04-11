using UnityEngine;

namespace Stuart
{
    public class BinBench : Bench
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
            
                RemoveItemFromPlayer(invent);
            
        }

        private void RemoveItemFromPlayer(Inventory invent)
        {
            if (invent != null && invent.CurrentItem != null)
                invent.RemoveItem();
        }
    }
}