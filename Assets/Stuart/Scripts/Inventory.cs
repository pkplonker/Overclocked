using System;
using UnityEngine;

namespace Stuart
{
    public class Inventory : MonoBehaviour
    {
        private ItemBaseSO currentItem;

        public ItemBaseSO CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                OnItemChanged?.Invoke(currentItem);
#if UNITY_EDITOR
                Debug.Log($"Picked up {value.objectName}");
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
    }
}