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
        [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
        private bool isPaused;
        public event Action<bool> OnPauseChanged; 
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

        private void Update()
        {
            if (Input.GetKeyDown(pauseKey))
                PauseGameToggle();
        }

        private void PauseGameToggle()
        {
            isPaused = !isPaused;
            SetPlayerMovement(false);
            OnPauseChanged?.Invoke(isPaused);
        }

        private void GameStart()
        {
            SetPlayerMovement(true);
            OnGameStart?.Invoke();
        }

        private void SetPlayerMovement(bool canMove)
        {
            foreach (var player in players) player.canMove = canMove;
        }
    }
}