using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Stuart;
using UnityEngine;

namespace Stuart
{


    public class FXController : MonoBehaviour
    {
        private List<TestingBench> testingBenches;
        [SerializeField] private ParticleSystem TestSuccessVFX;
        [SerializeField] private ParticleSystem TestStartVFX;
        [SerializeField] private ParticleSystem TestFailVFX;
        [SerializeField] private ParticleSystem smokeVFX;
        [SerializeField] private AudioClip TestSuccessSFX;
        [SerializeField] private AudioClip TestStartSFX;
        [SerializeField] private AudioClip TestFailSFX;
        [SerializeField] private AudioClip smokeSFX;
        private static ParticleSystem smokeVFX1;
        private AudioSource source;
        private static FXController instance;
        private void Start()
        {
            instance = this;
            source = GetComponent<AudioSource>();
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
            PlayClip(TestStartSFX);
        }

        private void TestSuccess(GameObject go)
        {
            SpawnVFX(go,TestSuccessVFX);
            PlayClip(TestSuccessSFX);
        }
        private void TestFail(GameObject go)
        {
            SpawnVFX(go,TestFailVFX);
            PlayClip(TestFailSFX);
        }

        private static void SpawnVFX(GameObject go,ParticleSystem VFX)=>Instantiate(VFX, go.transform);

        public static void SpawnFail(Transform t)
        {
            SpawnVFX(t.gameObject,smokeVFX1);
            PlayClip(instance.smokeSFX);
        }

        private static void PlayClip(AudioClip clip)
        {
            if (clip == null) return;
            instance.source.PlayOneShot(clip);
        }

        public static void StopSmoke()
        {
            
        }
    }
}