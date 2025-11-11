using UnityEngine;

// Trigger um die Sternschnuppe zu aktivieren
[RequireComponent(typeof(Collider2D))]
public class StartMovingPlatformTrigger : MonoBehaviour
{
    public MovingPlatform movingPlatform;  // Referenz zur Sternschnuppe
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            movingPlatform.ActivateMovement();
            Debug.Log("Moving platform activated!");
        }
    }
}

