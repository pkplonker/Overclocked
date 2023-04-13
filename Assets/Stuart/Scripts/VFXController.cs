using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Stuart;
using UnityEngine;

namespace Stuart
{


    public class VFXController : MonoBehaviour
    {
        private List<TestingBench> testingBenches;
        [SerializeField] private ParticleSystem TestSuccessVFX;
        [SerializeField] private ParticleSystem TestStartVFX;
        [SerializeField] private ParticleSystem TestFailVFX;
        [SerializeField] private ParticleSystem smokeVFX;
         private static ParticleSystem smokeVFX1;

        private void Start()
        {
            testingBenches = FindObjectsOfType<TestingBench>().ToList();
            foreach (var tb in testingBenches)
            {
                tb.OnTestStateChange += TestStateChange;
            }

            smokeVFX1 = smokeVFX;
        }

        private void TestStateChange(GameObject go, TestingStateData args)
        {
            switch (args.state)
            {
                case TestState.Started:
                    TestStarted(go);
                    break;
                case TestState.Complete:
                    TestSuccess(go);
                    break;
                case TestState.Failed:
                    TestFail(go);
                    break;
                case TestState.Aborted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TestStarted(GameObject go)
        {
            SpawnVFX(go,TestStartVFX);
        }

        private void TestSuccess(GameObject go)
        {
            SpawnVFX(go,TestSuccessVFX);
        }
        private void TestFail(GameObject go)
        {
            SpawnVFX(go,TestFailVFX);
        }

        private static void SpawnVFX(GameObject go,ParticleSystem VFX)=>Instantiate(VFX, go.transform);

        public static void SpawnFail(Transform t)
        {
            SpawnVFX(t.gameObject,smokeVFX1);
        }
    }
}