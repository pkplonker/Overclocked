using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{
    //todo change to state machine?
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        [SerializeField] private float gameStartInitialDelay = 1f;
        [SerializeField] private float gameStartCountdown = 3f;
        [SerializeField] private float gameTimeSeconds = 180f;
        private List<CharacterMovement> players;
        [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
        private bool isPaused;
        public event Action<bool> OnPauseChanged;
        public event Action<float> OnGameStartTimerTick;
        public event Action<float,float> OnGameTick;
        public event Action<bool> OnGameOver;
        public event Action OnGameStart;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(this);
            players = new List<CharacterMovement>(FindObjectsOfType<CharacterMovement>());
        }

        private void Start()=>StartCoroutine(GameStartCor());
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

        private void PauseGameToggle()=>SetPause(!isPaused);
        

        public void SetPause(bool state)
        {
            isPaused = state;
            SetPlayerMovement(!isPaused);
            OnPauseChanged?.Invoke(isPaused);
        }

        private void GameStart()
        {
            SetPlayerMovement(true);
#if UNITY_EDITOR
            Debug.Log("Game Start");
#endif
            OnGameStart?.Invoke();
            StartCoroutine(GameTick());
        }

        private IEnumerator GameTick()
        {
            var countdown = 0f;
            while (countdown < gameTimeSeconds)
            {
                countdown += Time.deltaTime;
                OnGameTick?.Invoke(countdown,gameTimeSeconds);
                yield return null;
            }
            SetGameOver();
        }

        private void SetGameOver()
        {
#if UNITY_EDITOR
            Debug.Log("GameOver");
#endif
            SetPlayerMovement(false);
            OnGameOver?.Invoke(false);
        }

        private void SetPlayerMovement(bool canMove)
        {
            foreach (var player in players) player.canMove = canMove;
        }
    }
}