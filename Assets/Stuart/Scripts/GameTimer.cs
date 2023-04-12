using System;
using System.Collections;
using UnityEngine;

namespace Stuart
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private float gameTime;
        private bool isPaused;
        private void Start()=>StartCoroutine(GameStartCor());
        private void OnEnable()=>GameController.Instance.OnPauseChanged += state => isPaused = state;
        public event Action<float> OnTimeChanged;
        private IEnumerator GameStartCor()
        {
            var countdown = 0f;
            while (countdown < gameTime)
            {
                if (!isPaused)
                {
                    countdown += Time.deltaTime;
                    OnTimeChanged?.Invoke(countdown);
                }
                yield return null;
            }
        }
    }
}