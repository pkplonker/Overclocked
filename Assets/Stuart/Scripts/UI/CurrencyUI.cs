using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Stuart
{
	public class CurrencyUI : MonoBehaviour
	{
		[SerializeField] private float _speed = 2.5f;
		[SerializeField] private TextMeshProUGUI text;
		private float currentAmount;
		private float targetAmount = 0;
		private Coroutine coroutine;

		private void Start()
		{
			text.text = "£0";
			JobFactory.JobCompleted += JobComplete;
			JobComplete(new JobWithTiming());
		}

		private void JobComplete(JobWithTiming job)
		{
			targetAmount += job.value;
			if (coroutine == null)
				StartCoroutine(UpdateTextCor());
		}
		private void UpdateText()=>text.text = $"£{currentAmount.ToString("n0")}";
		private IEnumerator UpdateTextCor()
		{
			var delta = targetAmount - currentAmount;
			while (targetAmount.ToString("n0") != currentAmount.ToString("n0"))
			{
				currentAmount = Mathf.MoveTowards(currentAmount, targetAmount,
					targetAmount > currentAmount ? delta / _speed * Time.deltaTime : -delta / _speed * Time.deltaTime);
				UpdateText();
				yield return null;
			}
			coroutine = null;
		}
	}
}