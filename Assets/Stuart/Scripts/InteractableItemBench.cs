using UnityEngine;

namespace Stuart
{
    public class InteractableItemBench : MonoBehaviour,IInteractable
    {
        [SerializeField] private ItemBaseSO item;
        public bool Interact(Interactor interactor)
        {
            Debug.Log($"Interacted with bench for {item.objectName}");
            if (!interactor.TryGetComponent<Inventory>(out var invent)) return true;
            if (invent.CurrentItem != null)
                invent.RemoveItem();
            invent.AddItem(item);
            return true;
        }
    }
}