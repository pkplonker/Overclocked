using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{
    public class CameraShake : MonoBehaviour
    {
        private Vector3 orignialPosition;
        public float shakeDuration = 0.2f;
        private float currentShake = 0f;
        public float shakeAmount = 0.7f;
        // Start is called before the first frame update
        void Start()
        {
            orignialPosition = transform.position;
            var testBenches = FindObjectsOfType<TestingBench>();
            foreach (var t in testBenches)
            {
                t.OnTestStateChange += TestStateChange;
            }
        }

        private void TestStateChange(GameObject arg1, TestingStateData arg2)
        {
            if(arg2.state==TestState.Failed) StartShake();
        }


        private void StartShake() => currentShake = shakeDuration;
        // Update is called once per frame
        void Update()
        {
            if (currentShake > 0)
            {
                transform.position = orignialPosition + Random.insideUnitSphere * shakeAmount;
                currentShake -= Time.deltaTime;
            }
            else
            {
                currentShake = 0f;
                transform.position = orignialPosition;
            }
        }
    }
}