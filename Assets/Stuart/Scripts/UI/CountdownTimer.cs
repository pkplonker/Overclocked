using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Stuart
{
	public class CountdownTimer : MonoBehaviour
	{
		private TextMeshProUGUI tmp;
		private void Awake() => tmp = GetComponent<TextMeshProUGUI>();
		private int previous = 99;
		private void Start()
		{
			GameController.Instance.OnGameStartTimerTick += GameTick;
			GameController.Instance.OnGameStart += ()=>Destroy(gameObject);
		}

		private void GameTick(float timer)
		{
			var val =TimeSpan.FromSeconds(timer).Seconds;
			if (val != previous)
			{
				previous = val;
				FXController.instance.OrderFail();
				transform.localScale = Vector3.zero;
				transform.DOScale(Vector3.one , 0.3f).SetEase(Ease.Flash);
			}
			tmp.text = (val+1).ToString();
		}
	}
}