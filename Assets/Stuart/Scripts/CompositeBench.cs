using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Stuart;
using UnityEngine;
using UnityEngine.Serialization;

public class CompositeBench : MonoBehaviour
{
	[SerializeField] private List<ItemBaseSO> itemsRequired;
	private List<Bench> benches;
	[SerializeField] private ItemBaseSO createdItem;

	private void Awake()
	{
		benches = GetComponentsInChildren<Bench>().ToList();
		foreach (var b in benches)
		{
			b.OnItemChanged += OnItemChanged;
		}
	}

	private void OnItemChanged(Bench bench, ItemBaseSO item)
	{
		var currentItems = benches.Select(b => b.CurrentItem).ToList();
		var required = new List<ItemBaseSO>(itemsRequired);
		required = required.Except(currentItems).ToList();
		if (required.Count > 0) return;
		foreach (var t in itemsRequired)
		{
			foreach (var b in benches)
			{
				if (b.CurrentItem != t) continue;
				b.RemoveItem();
				break;
			}
		}

		bench.AddItemToBench(createdItem);
	}
}