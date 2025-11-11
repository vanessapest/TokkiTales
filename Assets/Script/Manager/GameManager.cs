using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton: es gibt immer nur eine Instanz vom Game Manager
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }

    public GameObject gameOverScreen;

    public float lastRunTime { get; private set; }
    public int lastRunKeys { get; private set; }
    public int lastRunTotalKeys { get; private set; }

    private void Awake()
    {
        if (Instance != null) 
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Instance wird auf null gesetzt, falls der Game Manager doch irgendwann zerstört wird
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        // soll Spieler nur ein Leben haben?
        lives = 1; // nur ein Lebel
        Debug.Log("[GM] NewGame -> lives = " + lives);

        LoadLevel(1, 1); // erste Szene wird geladen
    }

    private void LoadLevel(int world, int stage)
    {
        // world und stage werden gemerkt -> hier: 1-1
        this.world = world;
        this.stage = stage;

        Debug.Log("[GM] LoadLevel " + world + "-" + stage);

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Level neu laden = Timer resetten + starten
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.ResetTimer();
            GameTimer.Instance.StartTimer();
        }

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void ResetLevel(float delay) // wird von DeathBarrier.cs aufgerufen
    {
        Debug.Log("[GM] ResetLevel(delay " + delay + ") called, scheduling Invoke");

        // Spieler ist tot, also Timer stoppen
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.PauseTimer();
        }

        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        Debug.Log("[GM] ResetLevel() INVOKED");

        if (lives > 0)
        {
            // keine Leben mehr -> Game Over Screen
            Debug.Log("[GM] GameOver() entered from ResetLevel()");
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("[GM] GameOver() entered");

        // Timer wird angehalten
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.PauseTimer();
        }

        if (gameOverScreen != null)
        {
            Debug.Log("[GM] Activating gameOverScreen");
            gameOverScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("[GM] gameOverScreen is NULL!");
        }
    }

    public void RetryLevel() // Restart-Button bei Game Over Screen
    {
        Debug.Log("[GM] RetryLevel() called");
        lives = 1;

        LoadLevel(world, stage);
    }

    public void SetLastRunStats(int collectedKeys, int totalKeys, float time) // wird von LevelGoal.cs aufgerufen
    {
        lastRunKeys = collectedKeys; // wie viele Keys gesammelt wurden
        lastRunTotalKeys = totalKeys; // wie viele Keys es insgesamt gab
        lastRunTime = time; // wie lange der Durchlauf gedauert hat

        Debug.Log($"[GM] Last run -> keys {collectedKeys}/{totalKeys}, time {time}");
    }
}
