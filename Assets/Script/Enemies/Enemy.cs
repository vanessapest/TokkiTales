using UnityEngine;

// Zusammenstoß von Spieler und Gegner
public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>(); // aus Player Script

            // prüfen, aus welcher Richtung die Kollision kommt
            // Spieler von oben auf Gegner -> also Richtung "down"
            if (collision.transform.DotTest(transform, Vector2.down)) 
            {
                player.Hit();
            }
        }
    }
}
