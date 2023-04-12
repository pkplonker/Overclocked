using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stuart
{
    public class CompositeBench : MonoBehaviour
    {
        [SerializeField] private List<RequiredItem> itemsRequired;
        [SerializeField] private CompositeItem createdItem;
        private List<Bench> benches;

        private void Awake()
        {
            benches = GetComponentsInChildren<Bench>().ToList();
            foreach (var b in benches) b.OnItemChanged += OnItemChanged;
        }

        private void OnItemChanged(Bench bench, ItemBaseSO item)
        {
            var currentItems = benches.Select(b => b.CurrentItem).ToList();
            var required = new List<RequiredItem>(itemsRequired);

            for (var i = required.Count - 1; i >= 0; i--)
            {
                foreach (var it in currentItems)
                {
                    if (it == null) continue;
                    if (required[i].type != it.type || required[i].value != it.value) continue;
                    CompositeItemTested tp = it as CompositeItemTested;
                    if (required[i].testedPartRequired)
                    {
                        if (tp != null && tp.isTestPass)
                        {
                            Debug.Log($"Found {required[i].type}");
                            required.RemoveAt(i);
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log($"Found {required[i].type}");
                        required.RemoveAt(i);
                        break;
                    }
                }
            }

            if (required.Count > 0) return;
            var created = Instantiate(createdItem);
            foreach (var t in itemsRequired)
            foreach (var b in benches)
            {
                if (b.CurrentItem == null) continue;
                if (b.CurrentItem.type != t.type || b.CurrentItem.value != t.value) continue;
                CompositeItem compositeItem = b.CurrentItem as CompositeItem;
                if (compositeItem != null)
                {
                    foreach (var subitem in compositeItem.subItems)
                    {
                        created.subItems.Add(new RequiredItem(subitem.type, subitem.value));
                    }
                }
                else
                {
                    created.subItems.Add(new RequiredItem(b.CurrentItem.type, b.CurrentItem.value));
                }
                
                b.RemoveItem();
                break;
            }

            bench.AddItemToBench(created);
        }
    }

    [Serializable]
    public struct RequiredItem
    {
        public ItemType type;
        public float value;
        public bool testedPartRequired;
        public RequiredItem(ItemType type, float value, bool testedPartRequired=false)
        {
            this.type = type;
            this.value = value;
            this.testedPartRequired = testedPartRequired;
        }
    }
}