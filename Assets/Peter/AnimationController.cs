using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stuart;

public class AnimationController : MonoBehaviour
{
    private Inventory invent;
    private ItemBaseSO previousItem = null;
    private ItemBaseSO nextItem = null;
    private Animator an;

    private void OnEnable()
    {
        an = GetComponentInChildren<Animator>();
        invent = GetComponent<Inventory>();
        invent.OnItemChanged += ItemChanged;
    }

    private void ItemChanged(ItemBaseSO item)
    {
        nextItem = item;
    }

    private void LateUpdate()
    {
        if(nextItem != previousItem)
        {
            if (previousItem == null && nextItem != null)
            {
                an.SetTrigger("pickup");
            }
            if (previousItem != null && nextItem == null)
            {
                an.SetTrigger("putDown");
            }

            previousItem = nextItem;
        }
    }
}
