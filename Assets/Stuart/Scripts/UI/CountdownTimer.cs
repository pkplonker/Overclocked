using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Stuart
{
	public class CountdownTimer : MonoBehaviour
	{
		private TextMeshProUGUI tmp;
		private void Awake() => tmp = GetComponent<TextMeshProUGUI>();

		private void Start()
		{
			GameController.Instance.OnGameStartTimerTick += GameTick;
			GameController.Instance.OnGameStart += ()=>Destroy(gameObject);
		}
		private void GameTick(float timer)=>tmp.text = string.Format($"{TimeSpan.FromSeconds(timer).Seconds}");
		
		
	}
}