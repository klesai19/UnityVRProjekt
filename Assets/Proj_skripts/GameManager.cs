using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
        public enum GameState { Idle, Running, Ended }
    
        [Header("Round Settings")]
    
        [SerializeField] private float roundDuration = 45f;
    
        [Header("UI")]
    
        [SerializeField] private TMP_Text stateText;
    
        [SerializeField] private TMP_Text scoreText;
    
        public GameState State { get; private set; } = GameState.Idle;
    
        public int Score { get; private set; }
    
        private float timer;
    
        public static GameManager Instance { get; private set; }
    
        private void Awake()
    
        {
    
            if (Instance != null) { Destroy(gameObject); return; }
    
            Instance = this;
    
        }
    
        private void Start()
    
        {
    
            SetState(GameState.Idle);
    
        }
    
        private void Update()
    
        {
    
            if (State != GameState.Running) return;
    
            timer -= Time.deltaTime;
    
            if (timer <= 0f)
    
            {
    
                EndRound();
    
            }
    
        }
    
        public void StartRound()
    
        {
    
            if (State == GameState.Running) return;
    
            Score = 0;
    
            timer = roundDuration;
    
            UpdateUI();
    
            SetState(GameState.Running);
    
        }
    
        public void EndRound()
    
        {
    
            if (State != GameState.Running) return;
    
            SetState(GameState.Ended);
    
        }
    
        public void AddScore(int amount)
    
        {
    
            if (State != GameState.Running) return;
    
            Score += amount;
    
            UpdateUI();
    
        }
    
        private void SetState(GameState newState)
    
        {
    
            State = newState;
    
            UpdateUI();
    
        }
    
        private void UpdateUI()
    
        {
    
            if (scoreText != null) scoreText.text = $"Score: {Score}";
    
            if (stateText != null)
    
            {
    
                stateText.text = State switch
    
                {
    
                    GameState.Idle => "Idle (bereit)",
    
                    GameState.Running => "Running",
    
                    GameState.Ended => "Game Over",
    
                    _ => "?"
    
                };
    
            }
    
        }
    
    }
     