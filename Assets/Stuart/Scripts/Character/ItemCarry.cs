using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{
    public class ItemCarry : MonoBehaviour
    {
        [SerializeField] private Transform carryTarget;
        private Inventory invent;
        private GameObject currentObject;
        private ItemBaseSO currentItem;
        private void OnValidate()
        {
#if UNITY_EDITOR
            if(carryTarget==null) Debug.LogWarning("CarryTarget cannot be null");
#endif
        }

        private void OnEnable()
        {
            invent = GetComponent<Inventory>();
            if (invent == null)
                throw new Exception("CarryTarget cannot be null");
            invent.OnItemChanged += ItemChanged;
        }

        private void ItemChanged(ItemBaseSO item)
        {
            if (item == currentItem) return;
            currentItem = item;
            if(currentObject!=null)
                Destroy(currentObject);
            if (currentItem != null)
                currentObject = Instantiate(currentItem.prefab, carryTarget.position, Quaternion.identity, carryTarget);
        }
    }
}
