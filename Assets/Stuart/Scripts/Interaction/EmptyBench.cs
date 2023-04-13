using UnityEngine;

namespace Stuart
{
	public class EmptyBench : Bench
	{
		private void OnValidate()
		{
			if (itemSpot == null)
				Debug.LogWarning("Missing itemspot");
		}

		public override void Interact(Interactor interactor)
		{
			if (!interactor.TryGetComponent<Inventory>(out var invent)) return;
			if (invent.CurrentItem != null)
			{
				if (CurrentItem != null)
				{
					var cached = CurrentItem;
					AddItemToBench(invent);
					AddItemToPlayerInvent(invent, cached);
				}
				else
					AddItemToBench(invent);
			}
			else
			{
				AddItemToPlayerInvent(invent, CurrentItem);
				RemoveItemFromBench(invent);
			}
		}
	}
}