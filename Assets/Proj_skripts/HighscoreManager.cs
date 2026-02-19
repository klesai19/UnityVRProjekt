using UnityEngine;
using TMPro;
public class HighscoreManager : MonoBehaviour

{
    private const string KEY_HIGHSCORE = "HIGHSCORE";
    [Header("UI")]
    [SerializeField] private GameObject highscorePanel;
    [SerializeField] private TMP_Text highscoreText;
    public int Highscore { get; private set; }
    private void Awake()
    {
        LoadHighscore();
        UpdateUI();
        if (highscorePanel != null) highscorePanel.SetActive(false);
    }

    private void OnEnable()
    {
            GameManagerEndedHook.OnRoundEnded += HandleRoundEnded;
    }
    private void OnDisable()
    {
        GameManagerEndedHook.OnRoundEnded -= HandleRoundEnded;
    }
    private void HandleRoundEnded(int score)
    {
        if (score > Highscore)
        {
            Highscore = score;
            SaveHighscore();
            UpdateUI();
        }
    }
    public void TogglePanel()
    {
        if (highscorePanel == null) return;
        bool newState = !highscorePanel.activeSelf;
        highscorePanel.SetActive(newState);
        if (newState) UpdateUI();
    }
    private void LoadHighscore()
    {
        Highscore = PlayerPrefs.GetInt(KEY_HIGHSCORE, 0);
    }
    private void SaveHighscore()
    {
        PlayerPrefs.SetInt(KEY_HIGHSCORE, Highscore);
        PlayerPrefs.Save();
    }
    private void UpdateUI()
    {
        if (highscoreText != null)
            highscoreText.text = $"Highscore: {Highscore}";

    }

}