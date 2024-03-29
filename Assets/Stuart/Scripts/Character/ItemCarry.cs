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
			if (carryTarget == null) Debug.LogWarning("CarryTarget cannot be null");
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
			if (currentObject != null)
				Destroy(currentObject);
			if (currentItem == null) return;
			currentObject = Instantiate(currentItem.GetPrefab(), carryTarget.position,
				currentItem.GetPrefab().transform.rotation, carryTarget);
			currentObject.GetComponent<Item>().itemSO = currentItem;
		}
	}
}