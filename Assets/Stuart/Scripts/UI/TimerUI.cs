using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Stuart
{


    public class TimerUI : MonoBehaviour
    {
        private TextMeshProUGUI tmp;
        private void Awake()=>tmp = GetComponent<TextMeshProUGUI>();
        private void Start()=>GameController.Instance.OnGameTick += GameTick;
        private void GameTick(float elapsed, float total)
        {
            var t = TimeSpan.FromSeconds( total-elapsed );
            tmp.text = string.Format($"{t.Minutes}:{t.Seconds}"); ;
            
        }
    }
}