using UnityEngine;

// Schlüssel aufsammeln
[RequireComponent(typeof(Collider2D))] // Objekt soll im Collider haben
public class KeyPickup : MonoBehaviour
{
    public int keyID = 0; // um Keys zu unterscheiden
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return; // wenn schon eingesammelt, dann abbrechen

        if (other.CompareTag("Player")) // Berührung mit Spieler?
        {
            collected = true;

            // Key Manager Bescheid geben
            KeyManager.Instance.CollectKey();

            // Key verschwinden lassen
            gameObject.SetActive(false);
        }
    }
}
