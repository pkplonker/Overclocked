using System;
using System.Collections;
using UnityEngine;
namespace Stuart
{
    public class TestingBench : EmptyBench
    {
        [SerializeField] private ItemBaseSO requiredItem;
        [SerializeField] private ItemBaseSO createdItem;
        [SerializeField] private ItemBaseSO failedItem;
        private Coroutine testTimerCor;
        [SerializeField] private float testTime=5f;
        [SerializeField] private float allowedTime=4f;
        public event Action<TestState> OnTestStateChange;
 
        private void OnValidate()
        {
#if UNITY_EDITOR
            if(itemSpot==null)
                Debug.LogWarning("Missing itemspot");
#endif
        }
        
        protected override void AddItemToBench(Inventory invent)
        {
            base.AddItemToBench(invent);
            TestTimer();
        }

        private void TestTimer()
        {
            StopTimer();
            if(CurrentItem==requiredItem)
                testTimerCor = StartCoroutine(TestTimerCoroutine());
        }

        private void StopTimer()
        {
            if (testTimerCor != null)
                StopCoroutine(testTimerCor);
        }

        protected override void RemoveItemFromBench(Inventory invent)
        {
            base.RemoveItemFromBench(invent);
            StopTimer();
        }
        private IEnumerator TestTimerCoroutine()
        {
            OnTestStateChange?.Invoke(TestState.Started);
            var countdown = 0f;
            while(countdown < testTime)
            {
                countdown += Time.deltaTime;
                Debug.Log(countdown);
                yield return null;
            }
            RemoveItem();
            AddItemToBench(createdItem);
            OnTestStateChange?.Invoke(TestState.Complete);
            countdown = 0f;
            while(countdown < testTime)
            {
                countdown += Time.deltaTime;
                Debug.Log(countdown);
                yield return null;
            }
            RemoveItem();
            RemoveItem();
            AddItemToBench(failedItem);
            OnTestStateChange?.Invoke(TestState.Failed);
            testTimerCor = null;
        }
    }
    public enum TestState
    {
        Started,
        Complete,
        Failed
    }
}

