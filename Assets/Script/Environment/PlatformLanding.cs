using UnityEngine;

// wenn der Spieler auf die Blumen landet -> weiterleiten an PlatformSequenceManager.cs
// Blumen sollen nacheinander aktiviert werden
[RequireComponent(typeof(Collider2D))] // muss Collider haben
public class PlatformLanding : MonoBehaviour
{
    public PlatformSequenceManager sequenceManager; // Manager steuert Aktion

    private bool alreadyTriggered = false; // verhindert, das Blumen mehrmals aktiviert werden

    // wenn Spieler kurz darauf landet -> durch Physikverzögerung
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryActivate(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryActivate(collision);
    }

    private void TryActivate(Collision2D collision)
    {
        // schon ausgelöst? abbrechen
        if (alreadyTriggered || !collision.collider.CompareTag("Player")) return;

        alreadyTriggered = true; // auf "benutzt", damit es nicht nochmal ausgelöst wird
        sequenceManager.OnFlowerLanded(gameObject); // an Manager: Spieler ist auf Blume gelandet
        Debug.Log(gameObject.name + " was landed on, progress notified.");
    }
}
