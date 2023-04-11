using UnityEngine;

namespace Stuart
{
    public class InteractableItemBench : Bench
    {
        public override void Interact(Interactor interactor)
        {
            Debug.Log($"Interacted with bench for {currentItem.objectName}");
            var invent = GetInvent(interactor);
            if (invent == null) return;
            if (invent.CurrentItem != null)
                invent.RemoveItem();
            invent.AddItem(currentItem);
        }
    }
}