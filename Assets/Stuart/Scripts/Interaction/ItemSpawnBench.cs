using UnityEngine;

namespace Stuart
{
	public class ItemSpawnBench : Bench
	{
		public override void Interact(Interactor interactor)
		{
			var invent = GetInvent(interactor);
			invent.AttemptDropItem();
			AddItemToPlayerInvent(invent, CurrentItem);
		}
	}
}