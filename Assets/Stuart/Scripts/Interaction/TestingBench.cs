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
		[SerializeField] private float testTime = 5f;
		[SerializeField] private float allowedTime = 4f;
		public event Action<GameObject, TestingStateData> OnTestStateChange;
		private float countdown = 0f;

		private void OnValidate()
		{
#if UNITY_EDITOR
			if (itemSpot == null)
				Debug.LogWarning("Missing itemspot");
#endif
		}

		protected override void AddItemToBench(Inventory invent)
		{
			if (invent.CurrentItem != null)
			{
				var tested = invent.CurrentItem as CompositeItemTested;
				if (invent.CurrentItem.type == requiredItemType && tested == null)
				{
					base.AddItemToBench(invent);
					TestTimer();
				}
				else
					FXController.instance.IncorrectItem();
			}
			else FXController.instance.IncorrectItem();
		}

		private void TestTimer()
		{
			StopTimer();
			testTimerCor = StartCoroutine(TestTimerCoroutine());
		}

		private void StopTimer()
		{
			if (testTimerCor != null)
			{
				StopCoroutine(testTimerCor);
				OnTestStateChange?.Invoke(currentSpawnedItem,
					new TestingStateData(TestState.Aborted, GetElapsedTime, testTime));
			}
		}

		protected override void RemoveItemFromBench(Inventory invent)
		{
			base.RemoveItemFromBench(invent);
			StopTimer();
		}

		private IEnumerator TestTimerCoroutine()
		{
			OnTestStateChange?.Invoke(currentSpawnedItem,
				new TestingStateData(TestState.Started, GetElapsedTime, testTime));
			var cachedItem = CurrentItem;
			countdown = 0f;
			while (countdown < testTime)
			{
				countdown += Time.deltaTime;
				//Debug.Log(countdown);
				yield return null;
			}

			RemoveItem();
			AddItemToBench(CreateTestedItem(cachedItem, createdItem, true));
			Debug.Log("TestingComplete");
			countdown = 0f;
			OnTestStateChange?.Invoke(currentSpawnedItem,
				new TestingStateData(TestState.Complete, GetElapsedTime, allowedTime));
			while (countdown < allowedTime)
			{
				countdown += Time.deltaTime;
				//Debug.Log(countdown);
				yield return null;
			}

			AddItemToBench(CreateTestedItem(cachedItem, createdItem, false));
			Debug.Log("Testing Failed");
			OnTestStateChange?.Invoke(currentSpawnedItem,
				new TestingStateData(TestState.Failed, GetElapsedTime, allowedTime));
			testTimerCor = null;
		}

		private ItemBaseSO CreateTestedItem(ItemBaseSO cachedItem, ItemBaseSO item, bool p1)
		{
			var newItem = Instantiate(item);
			var n = (CompositeItemTested) newItem;
			if (n == null)
			{
				Debug.LogError("Item composition error");
				return null;
			}

			n.isTestPass = p1;
			n.subItems = new List<RequiredItem>(((CompositeItem) cachedItem).subItems);
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