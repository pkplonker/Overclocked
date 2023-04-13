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
        public List<JobWithTiming> jobsSpawned = new();
        public static event Action OnWin; 
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
            jobsSpawned.Add(jobs[0]);
            jobs.RemoveAt(0);
            Debug.Log("Job added");
        }

        private void OnJobDelivered(CompositeItemTested item)
        {
            Debug.Log("Job completed");
            JobWithTiming? completedJob = null;
            foreach (var spawnedJob in jobsSpawned)
            {
                var matches = 0;
                foreach (var requiredItem in spawnedJob.job.requiredItems)
                {
                    foreach (var subItem in item.subItems)
                    {
                        if (subItem.type == requiredItem.type && subItem.value == requiredItem.value) matches++;
                        if (matches != spawnedJob.job.requiredItems.Count) continue;
                        completedJob = spawnedJob;
                        break;
                    }
                }
            }
            if (completedJob == null) return;
            JobCompleted?.Invoke((JobWithTiming)completedJob);
            Debug.Log("Validated job complete");
            CheckWin();
        }

        private void CheckWin()
        {
            if (jobs.Count == 0 && jobsSpawned.Count == 0) OnWin?.Invoke();
        }
    }
}
