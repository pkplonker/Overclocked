using System;
using UnityEngine;

namespace Stuart
{
    public class Inventory : MonoBehaviour
    {
     [SerializeField]   private ItemBaseSO currentItem;

        public ItemBaseSO CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                OnItemChanged?.Invoke(currentItem);
#if UNITY_EDITOR
                if (value != null)
                    Debug.Log($"Added {value.objectName} to inventory");
                else Debug.Log($"Removed item from inventory");
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