using UnityEngine;
using UnityEngine.SceneManagement;

// wenn Spieler das Ziel erreicht -> beenden des Levels
[RequireComponent(typeof(Collider2D))] // Trigger für die Tür 
public class LevelGoal : MonoBehaviour
{
    public string resultSceneName = "LevelComplete"; 

    private bool triggered = false; // verhindert, dass das Ziel mehrmals ausgelöst wird

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player")) // prüfen, ob es der Spieler ist 
            return;

        triggered = true;

        // Timer anhalten
        float time = 0f;
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.PauseTimer();
            time = GameTimer.Instance.GetElapsedSeconds();
        }

        // Keys aus dem KeyManager lesen 
        int collected = 0;
        int total = 0;
        if (KeyManager.Instance != null)
        {
            collected = KeyManager.Instance.GetCollectedKeys();
            total = KeyManager.Instance.GetTotalKeys();
        }

        // Stats an GameManager übergeben -> um in der nächsten Szene anzeigen zu lassen
        GameManager.Instance.SetLastRunStats(collected, total, time);

        // Highscore speichern 
        // GameManager.Instance.UpdateHighscore(time);

        // Timer-Objekt zerstören, damit UI nicht mit in die nächste Szene kommt
        if (GameTimer.Instance != null)
        {
            Destroy(GameTimer.Instance.gameObject);
        }

        // Ergebnis-Szene laden
        SceneManager.LoadScene(resultSceneName);
    }
}
