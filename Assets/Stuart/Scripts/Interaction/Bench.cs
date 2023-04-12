using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{
	public abstract class Bench : MonoBehaviour, IInteractable
	{
		[SerializeField] private ItemBaseSO currentItem;
		public ItemBaseSO CurrentItem
		{
			get => currentItem;
			private set { currentItem = value; OnItemChanged?.Invoke(this,currentItem); }

		}
		[SerializeField] protected Transform itemSpot;
		protected GameObject currentSpawnedItem;
		public event Action<Bench,ItemBaseSO> OnItemChanged;
		public abstract void Interact(Interactor interactor);
		public Transform GetTransform() => transform;

		protected static Inventory GetInvent(Interactor interactor)=>interactor.TryGetComponent<Inventory>(out var invent) ? invent : null;
		
		protected virtual void AddItemToBench(Inventory invent)
		{
			if (invent == null ||invent.CurrentItem == null) return;
			var itemToAdd= invent.RemoveItem();
			if (itemToAdd == null || itemToAdd==CurrentItem) return;
			CurrentItem = itemToAdd;
			SpawnItem();
		}

		protected void SpawnItem()
		{
			if(currentSpawnedItem!=null)
				Destroy(currentSpawnedItem);
			currentSpawnedItem = Instantiate(CurrentItem.prefab, itemSpot.position, Quaternion.identity, itemSpot);
			currentSpawnedItem.GetComponent<Item>().itemSO = CurrentItem;
		}

		public void AddItemToBench(ItemBaseSO item)
		{
			if (item == null || item==CurrentItem) return;
			currentItem = item;
			SpawnItem();
		}

		protected virtual void AddItemToPlayerInvent(Inventory invent,ItemBaseSO itemToAdd)
		{
			if (invent == null) return;
			if (invent.CurrentItem != null)
				invent.RemoveItem();
			invent.AddItem(itemToAdd);
		}
		protected virtual void RemoveItemFromBench(Inventory invent)
		{
			if (invent == null) return;
			if (invent.CurrentItem != null)
				invent.RemoveItem();
			invent.AddItem(CurrentItem);
			RemoveItem();
		}

		public void RemoveItem()
		{
			if (currentSpawnedItem != null) Destroy(currentSpawnedItem);
			CurrentItem = null;
		}
	}
}