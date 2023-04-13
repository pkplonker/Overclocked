using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Stuart
{

    public class JobFactory : MonoBehaviour
    {
        public List<JobWithTiming> jobs;
        public static event Action<JobWithTiming> JobAdded;
        public static event Action<JobWithTiming> JobCompleted;
        private JobBench jobBench;
        private void Awake()=>jobBench = FindObjectOfType<JobBench>();
        
        private void Start()
        {
            jobBench.OnJobCompleted += OnJobDelivered;
            GameController.Instance.OnGameTick += GameTick;
        }

        private void GameTick(float elapsed, float totalLevelTime)
        {
            if (jobs.Count == 0) return;
            if (!(elapsed > jobs[0].spawnTime)) return;
            JobAdded?.Invoke(jobs[0]);
            jobs.RemoveAt(0);
            Debug.Log("Job added");
        }

        private void OnJobDelivered(CompositeItemTested item)
        {
            Debug.Log("Job completed");
           // JobCompleted?.Invoke(item);
        }
    }
}
