using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        [SerializeField] private float gameStartInitialDelay = 1f;
        [SerializeField] private float gameStartCountdown = 3f;
        private List<CharacterMovement> players;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(this);
            players = new List<CharacterMovement>(FindObjectsOfType<CharacterMovement>());
        }

        private void Start()=>StartCoroutine(GameStartCor());
        public event Action<float> OnGameStartTimerTick;
        public event Action OnGameStart;

        private IEnumerator GameStartCor()
        {
            var countdown = 0f;
            while (countdown < gameStartInitialDelay)
            {
                countdown += Time.deltaTime;
                //Debug.Log(countdown);
                yield return null;
            }

            countdown = 0f;
            while (countdown < gameStartCountdown)
            {
                countdown += Time.deltaTime;
                OnGameStartTimerTick?.Invoke(gameStartCountdown - countdown);
                yield return null;
            }
            GameStart();
        }

        private void GameStart()
        {
            foreach (var player in players) player.canMove = true;
            OnGameStart?.Invoke();
        }
    }
}