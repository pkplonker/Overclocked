using UnityEngine;

namespace Stuart
{
    public class EmptyBench : Bench
    {
        private void OnValidate()
        {
#if UNITY_EDITOR
            if(itemSpot==null)
                Debug.LogWarning("Missing itemspot");
#endif
        }

        public override void Interact(Interactor interactor)
        {
#if UNITY_EDITOR
            Debug.Log("Interacted with bench ");
#endif
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