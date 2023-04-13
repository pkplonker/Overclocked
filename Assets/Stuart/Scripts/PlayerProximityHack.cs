using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

//hack class
namespace Stuart
{
	public class PlayerProximityHack : MonoBehaviour
	{
		private Canvas canvas;
		private float radius = 1.5f;
		private void Awake()=>canvas = GetComponent<Canvas>();
		private void Update()
		{
			canvas.enabled = false;
			var res = Physics.OverlapSphere(transform.position, radius).ToList();
			foreach (var r in res)
			{
				if (r.CompareTag("Player")) canvas.enabled = true;
			}
		}
	}
}