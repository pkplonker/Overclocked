using System;
using System.Collections;
using System.Collections.Generic;
using Stuart;
using TMPro;
using UnityEngine;

namespace Stuart
{
	public class UIComposite : MonoBehaviour
	{
		private TextMeshProUGUI tmp;
		private string text;

		public void Start()
		{
			var item = GetComponentInParent<Item>();
			if (item == null)
			{
				Debug.LogError("no item");
				return;
			}

			//Debug.Log(item.itemSO.objectName);
			tmp = GetComponentInChildren<TextMeshProUGUI>();
			transform.position += Vector3.up * 0.3f;
			tmp.text = UpdateText(item.itemSO);
			GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
		}


		private string UpdateText(ItemBaseSO item)
		{
			var s = "";
			var thisItem = item as CompositeItem;
			if (thisItem == null)
			{
				switch (thisItem.type)
				{
					case ItemType.HDD:
						s += $"{thisItem.value}GB {thisItem.type.ToString()}\n";
						break;
					case ItemType.GPU:
						s += $"{thisItem.value} {thisItem.type.ToString()}\n";
						break;
					case ItemType.RAM:
						s += $"{thisItem.value}GB {thisItem.type.ToString()}\n";
						break;
				}
				return s;
			}
			foreach (var sumItem in thisItem.subItems)
			{
				switch (sumItem.type)
				{
					case ItemType.HDD:
						s += $"{sumItem.value}GB {sumItem.type.ToString()}\n";
						break;
					case ItemType.GPU:
						s += $"{sumItem.value} {sumItem.type.ToString()}\n";
						break;
					case ItemType.RAM:
						s += $"{sumItem.value}GB {sumItem.type.ToString()}\n";
						break;
				}
			}
			return s.TrimEnd('\r', '\n');
		}
	}
}