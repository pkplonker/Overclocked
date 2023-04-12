using System;
using UnityEngine;

namespace Stuart
{
    public class Inventory : MonoBehaviour
    {
     [SerializeField]   private ItemBaseSO currentItem;
     [SerializeField] private float groundOffset = 0.15f;

        public ItemBaseSO CurrentItem
        {
            get => currentItem;
            private set
            {
                if (currentItem == value) return;
                currentItem = value;
                OnItemChanged?.Invoke(currentItem);
#if UNITY_EDITOR
                Debug.Log(value != null ? $"Added {value.objectName} to inventory" : "Removed item from inventory");
#endif
            }
        }
        public event Action<ItemBaseSO> OnItemChanged;

        public void AddItem(ItemBaseSO item)=>CurrentItem = item;
        public ItemBaseSO RemoveItem()
        {
            var item = CurrentItem;
            CurrentItem = null;
            return item;
        }
        public void AttemptDropItem()
        {
            if (CurrentItem == null) return;
            var item = RemoveItem();
            DropItem(item);
        }

        private void DropItem(ItemBaseSO item)
        {
            var go = Instantiate(item.prefab, new Vector3(transform.position.x, groundOffset, transform.position.z),
                Quaternion.identity);
            go.AddComponent<ItemPickup>();
        }
    }
}