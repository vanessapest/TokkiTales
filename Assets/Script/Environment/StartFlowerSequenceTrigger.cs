using UnityEngine;

// Auslöser, damit die Blumen Sequence startet
[RequireComponent(typeof(Collider2D))] // muss Collider haben
public class StartFlowerSequenceTrigger : MonoBehaviour
{
    public PlatformSequenceManager sequenceManager; // mit Manager verbinden

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            sequenceManager.BeginSequence(); // PlatformSequenceManager.cs
        }
    }
}
