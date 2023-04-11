using UnityEngine;

namespace Stuart
{
    public class InteractableItemBench : MonoBehaviour,IInteractable
    {
        [SerializeField] private ItemBaseSO item;
        public bool Interact(Interactor interactor)
        {
            Debug.Log($"Interacted with bench for {item.objectName}");
            return true;
        }
    }
}