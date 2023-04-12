using UnityEngine;

namespace Stuart
{
    public class ItemSpawnBench : Bench
    {
        public override void Interact(Interactor interactor)
        {
            Debug.Log($"Interacted with bench for {CurrentItem.objectName}");
            var invent = GetInvent(interactor);
            invent.AttemptDropItem();
            AddItemToPlayerInvent(invent, CurrentItem);
        }
    }
}