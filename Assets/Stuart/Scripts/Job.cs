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
        public int value;
        public static bool operator ==(JobWithTiming c1, JobWithTiming c2)=>(c1.job == c2.job);
        public static bool operator !=(JobWithTiming c1, JobWithTiming c2) =>(c1.job != c2.job);
        
    }
}
