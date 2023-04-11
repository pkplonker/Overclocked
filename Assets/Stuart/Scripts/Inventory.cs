using System;
using UnityEngine;

namespace Stuart
{
    public class Inventory : MonoBehaviour
    {
        public event Action<ItemBaseSO> OnItemChanged;
        private ItemBaseSO currentItem;
        public ItemBaseSO CurrentItem
        {
            get => currentItem;
            set
            {
                currentItem = value;
                OnItemChanged?.Invoke(currentItem);
            }
        }
        public void AddItem(ItemBaseSO item)=>CurrentItem = item;
        public ItemBaseSO AddItem()
        {
            var item = CurrentItem;
            CurrentItem = null;
            return item;
        }
    }
}