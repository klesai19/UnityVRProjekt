using UnityEngine;

using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState { Idle, Running, Ended }
    [Header("Round Settings")]
    [SerializeField] private float roundDuration = 45f;
    [Header("UI")]
    [SerializeField] private TMP_Text stateText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject gameOverPanelOrText; 
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
        UpdateTimerUI();
        if (timer <= 0f)
        {
            timer = 0f;
            EndRound();
        }
    }
    public void StartRound()
    {
        if (State == GameState.Running) return;
        Score = 0;
        timer = roundDuration;
        if (gameOverPanelOrText != null)
            gameOverPanelOrText.SetActive(false);
        SetState(GameState.Running);
        UpdateUI();
        UpdateTimerUI();
    }
    public void EndRound()
    {
        if (State != GameState.Running) return;
        SetState(GameState.Ended);
        if (gameOverPanelOrText != null)
            gameOverPanelOrText.SetActive(true);
        UpdateUI();
        UpdateTimerUI();
        GameManagerEndedHook.RaiseRoundEnded(Score);
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
                GameState.Ended => "Ended",
                _ => "?"
            };
        }
    }
    private void UpdateTimerUI()
    {
        if (timerText == null) return;
        int seconds = Mathf.CeilToInt(timer);
        timerText.text = $"Time: {seconds:00}";
    }
    private void OnEnable()
    {
        WormHitEvents.OnWormHit += HandleWormHit;
    }
    private void OnDisable()
    {
        WormHitEvents.OnWormHit -= HandleWormHit;
    }

    private void HandleWormHit(int points)
    {
        AddScore(points);
    }
}
 