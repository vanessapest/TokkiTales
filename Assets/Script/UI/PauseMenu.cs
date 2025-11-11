using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject pausePanel; // das PauseMenuPanel
    public GameTimer timer;       // Referenz auf dein GameTimer script

    [Header("Buttons")]
    public Button continueButton;
    public Button restartButton;
    public Button exitButton;
    public Button musicButton;

    [Header("Texts")]
    public TextMeshProUGUI musicButtonText;

    private bool isPaused = false;
    private bool musicEnabled = true;

    private void Awake()
    {
        if (timer == null && GameTimer.Instance != null)
        {
            timer = GameTimer.Instance;
        }
    }

    private void Start()
    {
        // Panel sicher aus
        pausePanel.SetActive(false);

        // Button-Klicks verbinden
        continueButton.onClick.AddListener(OnContinue);
        restartButton.onClick.AddListener(OnRestart);
        exitButton.onClick.AddListener(OnExit);
        musicButton.onClick.AddListener(OnMusicToggle);

        UpdateMusicButtonText();
    }

    private void Update()
    {
        // Taste prüfen
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);

        Time.timeScale = 0f;     // Welt anhalten
        timer.PauseTimer();      // Timer stoppen (falls du den Timer pausieren willst)
    }

    private void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);

        Time.timeScale = 1f;
        timer.ResumeTimer();
    }

    private void OnContinue()
    {
        ResumeGame();
    }

    private void OnRestart()
    {
        isPaused = false;
        pausePanel.SetActive(false);

        // Zeit wieder normal laufen lassen, bevor wir reloaden
        Time.timeScale = 1f;

        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.ResetTimer();
            GameTimer.Instance.StartTimer();
        }

        // aktuelle Scene neu laden
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    private void OnExit()
    {
        isPaused = false;
        pausePanel.SetActive(false);

        // Zeit normalisieren
        Time.timeScale = 1f;

        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.PauseTimer();
        }

        // "Menu"-Scene laden
        SceneManager.LoadScene("Menu");
    }

    private void OnMusicToggle()
    {
        if (MusicPlayer.Instance == null)
            return;

        musicEnabled = !musicEnabled;
        MusicPlayer.Instance.SetMusicEnabled(musicEnabled);
        UpdateMusicButtonText();
    }

    private void UpdateMusicButtonText()
    {
        if (musicButtonText != null)
        {
            musicButtonText.text = musicEnabled ? "Music: ON" : "Music: OFF";
        }
    }
}

