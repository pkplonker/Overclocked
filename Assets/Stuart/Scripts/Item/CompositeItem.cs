using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{
    [CreateAssetMenu(fileName = "ItemBase", menuName = "SO/Items/Composite", order = 2)]
    public class CompositeItem : ItemBaseSO
    {
        public List<RequiredItem> subItems;
        public bool isTestPass;
    }
}