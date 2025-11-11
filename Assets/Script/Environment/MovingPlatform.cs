using UnityEngine;

// bewegt die Sternschnuppe
public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 5f;        
    public bool startActive = false; // ob sie sich schon von Anfang an bewegt

    private bool isActive;

    private Rigidbody2D rb; // Physik Komponente der Plattform

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // merkt sich die Plattform
        isActive = startActive; // und schaut ob sie aktiv sein soll
    }

    private void FixedUpdate()
    {
        if (!isActive)
        {
            // nicht aktiv, stehen bleiben
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // aktiv -> nach links schieben
        rb.linearVelocity = new Vector2(-moveSpeed, 0f);
    }

    // von außen aufgerufen um Trigger zu starten -> StartMovingPlatformTrigger.cs
    public void ActivateMovement()
    {
        isActive = true;
    }
}