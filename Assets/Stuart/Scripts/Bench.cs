using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Stuart
{
	public abstract class Bench : MonoBehaviour, IInteractable
	{
		[SerializeField] protected ItemBaseSO currentItem;
		[SerializeField] protected Transform itemSpot;
		protected GameObject currentSpawnedItem;
		public abstract void Interact(Interactor interactor);

		protected static Inventory GetInvent(Interactor interactor)=>interactor.TryGetComponent<Inventory>(out var invent) ? invent : null;
		
		protected virtual void AddItemToBench(Inventory invent)
		{
			if (invent == null ||invent.CurrentItem == null) return;
			currentItem= invent.RemoveItem();
			if (currentItem == null) return;
			currentSpawnedItem = Instantiate(currentItem.prefab, itemSpot.position, Quaternion.identity, transform);
			return;
		}
		protected virtual void AddItemToPlayerInvent(Inventory invent)
		{
			
		}
	}
}