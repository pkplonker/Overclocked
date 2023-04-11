using UnityEngine;

namespace Stuart
{
    public class InteractableItemBench : Bench
    {
        public override void Interact(Interactor interactor)
        {
            Debug.Log($"Interacted with bench for {CurrentItem.objectName}");
            var invent = GetInvent(interactor);
            AddItemToPlayerInvent(invent, CurrentItem);
        }
    }
}