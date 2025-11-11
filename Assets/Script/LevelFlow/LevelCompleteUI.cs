using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

// UI, wenn Spieler Level abgeschlossen hat
public class LevelCompleteUI : MonoBehaviour
{
    public TextMeshProUGUI keysText;
    public TextMeshProUGUI timeText;
    public Button restartButton;
    public Button exitButton;

    private void Start()
    {
        // Daten vom GameManager holen
        var gm = GameManager.Instance;

        if (gm != null)
        {
            // Keys anzeigen
            keysText.text = $"Keys: {gm.lastRunKeys} / {gm.lastRunTotalKeys}";

            // Zeit formatieren
            float t = gm.lastRunTime;
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            timeText.text = $"Time: {minutes:00}:{seconds:00}";

            // Bestzeit 
            string levelKey = "best_" + gm.world + "-" + gm.stage;
        }

        // Buttons verdrahten
        restartButton.onClick.AddListener(OnRestart);
        exitButton.onClick.AddListener(OnExit);
    }

    private void OnRestart()
    {
        // vorhandene Logik
        GameManager.Instance.RetryLevel();
    }

    private void OnExit()
    {
        // kein Timer im Menü
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.PauseTimer();
        }

        SceneManager.LoadScene("Menu");
    }
}
