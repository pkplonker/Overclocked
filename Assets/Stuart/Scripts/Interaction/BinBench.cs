using UnityEngine;

namespace Stuart
{
	public class BinBench : Bench
	{
		public override void Interact(Interactor interactor)
		{
			if (!interactor.TryGetComponent<Inventory>(out var invent)) return;
			RemoveItemFromPlayer(invent);
		}

		private void RemoveItemFromPlayer(Inventory invent)
		{
			if (invent != null && invent.CurrentItem != null)
				invent.RemoveItem();
		}
	}
}