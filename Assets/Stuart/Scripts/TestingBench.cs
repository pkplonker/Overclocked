using System.Collections;
using System.Threading;
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

        private void OnValidate()
        {
            if(itemSpot==null)
                Debug.LogWarning("Missing itemspot");
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
            var countdown = 0f;
            while(countdown < testTime)
            {
                countdown += Time.deltaTime;
                Debug.Log(countdown);
                yield return null;
            }
            Debug.Log("Countdown complete");
            RemoveItem();
            Debug.Log("Removed raw Item");
            AddItemToBench(createdItem);
            Debug.Log("Created tested Item");

            countdown = 0f;
            while(countdown < testTime)
            {
                countdown += Time.deltaTime;
                Debug.Log(countdown);
                yield return null;
            }
            RemoveItem();
            Debug.Log("RemovedItem");
            RemoveItem();
            Debug.Log("Removed raw Item");
            AddItemToBench(failedItem);
            Debug.Log("Created failed Item");
            testTimerCor = null;
        }
    }
}