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
        private int jobsComplete = 0;
        private int jobsRequired = 0;
        private void Start()
        {
            JobBench.OnJobCompleted += OnJobDelivered;
            GameController.Instance.OnGameTick += GameTick;
            jobsRequired = jobs.Count;
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

        private void OnJobDelivered(Transform t, CompositeItemTested item)
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

            if (completedJob == null)
            {
                FXController.instance.OrderFail();
                return;
            }

            jobsComplete++;
            JobCompleted?.Invoke((JobWithTiming)completedJob);
            Debug.Log("Validated job complete");
            CheckWin();
        }

        private void CheckWin()
        {
            if (jobsComplete == jobsRequired)
            {
                OnWin?.Invoke();
                Debug.Log("All jobs complete");
            }
        }
    }
}
