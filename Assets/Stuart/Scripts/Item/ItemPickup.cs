using System;
using System.Collections;
using System.Collections.Generic;
using Stuart;
using UnityEngine;

namespace Stuart
{
	[RequireComponent(typeof(BoxCollider))]
	public class ItemPickup : MonoBehaviour, IInteractable
	{
		private void Awake()
		{
			var bc = GetComponent<BoxCollider>();
			bc.enabled = true;
			bc.isTrigger = true;
		}
		public void Interact(Interactor interactor)
		{
			if (!interactor.TryGetComponent<Inventory>(out var invent)) return;
			invent.AttemptDropItem();
			var itemToAdd = GetComponent<Item>().itemSO;
			invent.AddItem(itemToAdd);
			Destroy(gameObject);
		}
		public Transform GetTransform() => transform;
	}
}