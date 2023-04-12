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
        public event Action<TestingStateData> OnTestStateChange;
        private float countdown = 0f;
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
            {
                StopCoroutine(testTimerCor);
                OnTestStateChange?.Invoke(new TestingStateData(TestState.Aborted,GetElapsedTime,testTime));
            }
            
        }

        protected override void RemoveItemFromBench(Inventory invent)
        {
            base.RemoveItemFromBench(invent);
            StopTimer();
        }
        private IEnumerator TestTimerCoroutine()
        {
            OnTestStateChange?.Invoke(new TestingStateData(TestState.Started,GetElapsedTime,testTime));
            countdown = 0f;
            while(countdown < testTime)
            {
                countdown += Time.deltaTime;
                //Debug.Log(countdown);
                yield return null;
            }
            RemoveItem();
            AddItemToBench(createdItem);
            countdown = 0f;
            OnTestStateChange?.Invoke(new TestingStateData(TestState.Complete,GetElapsedTime,allowedTime));
            while(countdown < allowedTime)
            {
                countdown += Time.deltaTime;
                //Debug.Log(countdown);
                yield return null;
            }
            RemoveItem();
            RemoveItem();
            AddItemToBench(failedItem);
            OnTestStateChange?.Invoke(new TestingStateData(TestState.Failed,GetElapsedTime,allowedTime));
            testTimerCor = null;
        }

        private float GetElapsedTime() => countdown;

    }
    public enum TestState
    {
        Started,
        Complete,
        Failed,
        Aborted
    }

    public struct TestingStateData
    {
        public TestState state;
        public Func<float> getRemainingTime;
        public float totalTime;

        public TestingStateData(TestState state, Func<float> getRemainingTime, float totalTime)
        {
            this.state = state;
            this.getRemainingTime = getRemainingTime;
            this.totalTime = totalTime;
        }
    }
}

