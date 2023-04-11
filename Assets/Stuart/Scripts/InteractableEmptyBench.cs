using System;
using UnityEngine;

namespace Stuart
{
    public class InteractableEmptyBench : MonoBehaviour ,IInteractable
    {
        private ItemBaseSO item;
        [SerializeField] private Transform itemSpot;
        private GameObject spawnedItem;

        private void OnValidate()
        {
            if(itemSpot==null)
                Debug.LogWarning("Missing itemspot");
        }

        public bool Interact(Interactor interactor)
        {
            Debug.Log($"Interacted with bench ");
            if (!interactor.TryGetComponent<Inventory>(out var invent)) return true;
            return item == null ? AddItemToBench(invent) : RemoveItemFromBench(invent);
        }

        private bool RemoveItemFromBench(Inventory invent)
        {
            //todo 
            return false;
        }

        private bool AddItemToBench(Inventory invent)
        {
            if (invent.CurrentItem == null) return false;
            item= invent.RemoveItem();
            if (item == null) return false;
            spawnedItem = Instantiate(item.prefab, itemSpot.position, Quaternion.identity, transform);
            return true;
        }
        
    }
}