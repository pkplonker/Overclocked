using System;
using System.Collections;
using System.Collections.Generic;
using Stuart;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private float amount = 0;
    private void Start()
    {
        JobFactory.JobCompleted += JobComplete;
        JobComplete(new JobWithTiming());
    }

    private void JobComplete(JobWithTiming job)
    {
        amount += job.value;
        text.text = $"${(int)amount}";
    }
}
