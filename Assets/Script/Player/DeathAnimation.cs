using System.Collections;
using UnityEngine;

// nach dem Tod des Players soll eine Animation abgespielt werden
public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite(); // Aussehen anpassen
        DisablePhysics(); // Bewegung und Physik deaktivieren
        StartCoroutine(Animate()); // Sprung-Animation starten
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10; // sorgt dafür, dass das Bild vor anderen Objekten sichtbar bleibt

        if (deadSprite != null)
        {
            spriteRenderer.sprite = deadSprite; 
        }
    }

    private void DisablePhysics()
    {
        // alle Collider deaktivieren
        foreach (var col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }

        // Physik anhalten
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f; // Start Impuls nach oben
        float gravity = -36f;

        // velocity = wie schnell sich das Objekt in jede Richtung bewegt
        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime; // bewege das Objekt in Richtung seiner aktuellen Geschwindigkeit
            velocity.y += gravity * Time.deltaTime; // gravity ist negativ, also zieht sich das Objekt nach unten
            elapsed += Time.deltaTime; // die Zeit die seit der letzten Frame vergangen ist
            yield return null; // warte bis zur nächsten Frame, bis du weitermachst
        }
    }
}
