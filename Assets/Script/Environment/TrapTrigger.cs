using UnityEngine;

// Auslöser für den Fallboden
[RequireComponent(typeof(Collider2D))] // Trigger wichtig
public class TrapTrigger : MonoBehaviour
{
    public TrapGround trapGround; // Bezug zu TrapGround.cs
    private bool triggered = false; // verhindert, dass Trigger mehrmals ausgelöst wird

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return; // schon ausgelöst?

        if (other.CompareTag("Player"))
        {
            triggered = true;
            trapGround.StartTrapSequence(); // ruft beim TrapGround-Objekt die Methode auf
            Debug.Log("Trap triggered!");
        }
    }
}