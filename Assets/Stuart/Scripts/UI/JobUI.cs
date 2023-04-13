using System;
using System.Collections;
using System.Collections.Generic;
using Stuart;
using UnityEngine;

namespace Stuart
{
	public class JobUI : MonoBehaviour
	{
		[SerializeField] private Transform container;
		[SerializeField] private JobUISlot jobUISlotPrefab;
		private List<JobUISlot> spawnedUI = new();

		private void Start()
		{
			JobFactory.JobAdded += SpawnJob;
			JobFactory.JobCompleted += JobComplete;
		}

		private void JobComplete(JobWithTiming job)
		{
			foreach (var ui in spawnedUI)
			{
				Debug.Log("Checking job");
				if (ui == null) continue;
				if (ui.job != job) continue;
				Debug.Log("Found job to delete");
				Destroy(ui.gameObject);
			}
		}

		private void SpawnJob(JobWithTiming job)
		{
			var go = Instantiate(jobUISlotPrefab, container);
			go.Init(job);
			spawnedUI.Add(go);
			FXController.instance.OrderFail();
		}
	}
}