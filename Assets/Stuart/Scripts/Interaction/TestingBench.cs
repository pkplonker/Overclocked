using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stuart
{
    public class TestingBench : EmptyBench
    {
        [SerializeField] private ItemType requiredItemType;
        [SerializeField] private ItemBaseSO createdItem;
        private Coroutine testTimerCor;
        [SerializeField] private float testTime=5f;
        [SerializeField] private float allowedTime=4f;
        public event Action<Transform, TestingStateData> OnTestStateChange;
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
            if(CurrentItem.type ==requiredItemType)
                testTimerCor = StartCoroutine(TestTimerCoroutine());
        }

        private void StopTimer()
        {
            if (testTimerCor != null)
            {
                StopCoroutine(testTimerCor);
                OnTestStateChange?.Invoke(itemSpot,new TestingStateData(TestState.Aborted,GetElapsedTime,testTime));
            }
            
        }

        protected override void RemoveItemFromBench(Inventory invent)
        {
            base.RemoveItemFromBench(invent);
            StopTimer();
        }
        private IEnumerator TestTimerCoroutine()
        {
            OnTestStateChange?.Invoke(itemSpot, new TestingStateData(TestState.Started,GetElapsedTime,testTime));
            var cachedItem = CurrentItem;
            countdown = 0f;
            while(countdown < testTime)
            {
                countdown += Time.deltaTime;
                //Debug.Log(countdown);
                yield return null;
            }
            RemoveItem();
            AddItemToBench(CreateTestedItem(cachedItem,createdItem, true));
            countdown = 0f;
            OnTestStateChange?.Invoke(itemSpot, new TestingStateData(TestState.Complete,GetElapsedTime,allowedTime));
            while(countdown < allowedTime)
            {
                countdown += Time.deltaTime;
                //Debug.Log(countdown);
                yield return null;
            }
            
            AddItemToBench(CreateTestedItem(cachedItem,createdItem, false));
            OnTestStateChange?.Invoke(itemSpot, new TestingStateData(TestState.Failed,GetElapsedTime,allowedTime));
            testTimerCor = null;
        }

        private ItemBaseSO CreateTestedItem(ItemBaseSO cachedItem,ItemBaseSO item, bool p1)
        {
             var newItem =  Instantiate(item);
             var n = (CompositeItemTested)newItem;
             if (n == null)
             {
                 Debug.LogError("Item composition error");
                 return null;
             }
             n.isTestPass = p1;
             n.subItems = new List<RequiredItem>(((CompositeItem)cachedItem).subItems);
             return n;
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

