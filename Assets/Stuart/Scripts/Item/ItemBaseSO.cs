using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Stuart
{
    [CreateAssetMenu(fileName = "ItemBase", menuName = "SO/Items/Base", order = 1)]
    public class ItemBaseSO : ScriptableObject
    {
        [SerializeField] protected GameObject prefab;
        public string objectName;
        public ItemType type = ItemType.NULL;
        public float value = 0;
        public virtual GameObject GetPrefab() => prefab;

    }
    public enum ItemType
    {
        NULL,
        Motherboard,
        PSU,
        HDD,
        GPU,
        RAM,
        Case,
        MotherboardAssembly,
        FinalAssembly
    }

}