using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{
	public class Item : MonoBehaviour
	{
		public ItemBaseSO itemSO;

		[SerializeField] private UIComposite compUIPrefab;

		private void Start()
		{
			Instantiate(compUIPrefab, transform.position, Quaternion.identity, transform);
			var obj = itemSO as CompositeItemTested;
			if (obj == null) return;
			if (!obj.isTestPass) FXController.SpawnFail(transform);
		}
	}
}