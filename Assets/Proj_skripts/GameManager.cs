using UnityEngine;

using TMPro;
using UnityEngine.InputSystem;

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
    [SerializeField] private GameObject tempPanel;
    
    [SerializeField] private InputActionReference toggleMenuAction;
    
    
    [Header("Positioning of Main Camera (XR Origin)")] 
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distanceInFront = 1.5f;
    [SerializeField] private float heightOffset = 0.5f;
    [SerializeField] private float rightOffset = 0.5f;

    private Canvas panelCanvas;

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

        if (tempPanel != null)
        {
            CanvasGroup cg = tempPanel.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = tempPanel.AddComponent<CanvasGroup>();

            // hide menu at start
            cg.alpha = 0f;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
        
        panelCanvas = tempPanel.GetComponentInChildren<Canvas>(true);
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
        
        if (tempPanel != null && panelCanvas.enabled)
        {
            UpdatePanelPosition();
        }
    }
    
    private void UpdatePanelPosition()
    {
        Vector3 forward = playerTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = playerTransform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 targetPos = playerTransform.position
                            + forward * distanceInFront
                            + right * rightOffset;

        targetPos.y = playerTransform.position.y + heightOffset;

        tempPanel.transform.position = targetPos;

        Vector3 euler = playerTransform.eulerAngles;
        tempPanel.transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
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
        if (toggleMenuAction != null)
        {
            toggleMenuAction.action.Enable();
            toggleMenuAction.action.performed += OnToggle;
        }
    }
    private void OnDisable()
    {
        WormHitEvents.OnWormHit -= HandleWormHit;
        if (toggleMenuAction != null)
        {
            toggleMenuAction.action.performed -= OnToggle;
            toggleMenuAction.action.Disable();
        }
    }

    private void HandleWormHit(int points)
    {
        AddScore(points);
    }
    
    private void OnToggle(InputAction.CallbackContext ctx)
    {
        if (tempPanel != null)
            ToggleTempPanel();
    }
    
    public void ToggleTempPanel()
    {
        if (tempPanel == null) return;

        CanvasGroup cg = tempPanel.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = tempPanel.AddComponent<CanvasGroup>();

        bool visible = cg.alpha > 0.5f;

        cg.alpha = visible ? 0f : 1f;
        cg.interactable = !visible;
        cg.blocksRaycasts = !visible;
    }
    
    
}
 