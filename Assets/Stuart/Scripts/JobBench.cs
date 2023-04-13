using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Stuart
{
    public class JobBench : BinBench
    {
        public static event Action<Transform, CompositeItemTested> OnJobCompleted;
        public override void Interact(Interactor interactor)
        {
            if (!interactor.TryGetComponent<Inventory>(out var invent)) return;
            RemoveItemFromPlayer(invent);
        }
        private void RemoveItemFromPlayer(Inventory invent)
        {
            if (invent == null || invent.CurrentItem == null ||
                invent.CurrentItem.type != ItemType.FinalAssembly) return;
            var comp = invent.CurrentItem as CompositeItemTested;
            if (comp == null) return;
            if (!comp.isTestPass) return;
            invent.RemoveItem();
            Debug.Log("Final assembly added to job bench");
            OnJobCompleted?.Invoke(transform,comp);
            FXController.instance.OrderComplete();

        }
    }
}