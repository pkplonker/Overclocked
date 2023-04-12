using UnityEngine;

namespace Stuart
{
    [CreateAssetMenu(fileName = "ItemBase", menuName = "SO/Items/CompositeTested", order = 3)]
    public class CompositeItemTested : CompositeItem
    {
        public bool isTestPass;
       [SerializeField] private GameObject failedPrefab;
       [SerializeField] private GameObject passedPrefab;

       public override GameObject GetPrefab() => isTestPass ? passedPrefab : failedPrefab;
    }
}