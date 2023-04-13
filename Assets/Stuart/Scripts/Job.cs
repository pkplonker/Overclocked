using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{


    [CreateAssetMenu(fileName = "Job", menuName = "SO/Jobs/Basic")]
    public class Job : ScriptableObject
    {
        public List<ItemBaseSO> requiredItems;
    }
[Serializable]
    public struct JobWithTiming
    {
        public Job job;
        public float spawnTime;
    }
}
