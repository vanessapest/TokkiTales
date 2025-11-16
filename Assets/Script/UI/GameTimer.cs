using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// misst die Spielzeit in Sekunden
public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }
    public TextMeshProUGUI timerText;

    private float elapsed;
    private bool counting = true;

    private void Awake()
    {
        // Singleton-Pattern wie bei GameManager
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // bleibt über Scene Loads bestehen

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            Destroy(gameObject);
            Instance = null;
        }
    }

    private void Update()
    {
        if (!counting)
            return;

        // wichtig: benutze Time.deltaTime, damit die Zeit bei Pause (timeScale=0) stehen bleibt
        elapsed += Time.deltaTime;

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(elapsed / 60f);
            int seconds = Mathf.FloorToInt(elapsed % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void StartTimer()
    {
        counting = true;
    }

    public void PauseTimer()
    {
        counting = false;
    }

    public void ResumeTimer()
    {
        counting = true;
    }

    public void ResetTimer()
    {
        elapsed = 0f;
    }

    public float GetElapsedSeconds()
    {
        return elapsed;
    }
}

