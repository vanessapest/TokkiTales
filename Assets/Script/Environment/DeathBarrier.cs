using UnityEngine;

// wenn Spieler runter fällt
public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false); // Spieler wird deaktiviert
            GameManager.Instance.ResetLevel(1.5f); // Game Manager startet das Level nochmal
        }
        else
        {
            Destroy(other.gameObject); 
        }
    }
}
