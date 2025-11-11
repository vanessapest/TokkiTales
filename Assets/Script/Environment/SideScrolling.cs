using UnityEngine;

// Kamera verfolgt den Spieler
public class SideScrolling : MonoBehaviour
{
    // um den Spieler im Spiel zu merken
    // Transform: Zugriff auf die Position des Spielers
    private Transform player;

    private void Awake()
    {
        // findet Spieler und speichert die Position
        player = GameObject.FindWithTag("Player").transform; 
    }

    // damit die Kamera erst nachdem der Spieler sich bewegt hat, auf die aktuelle Position reagiert
    private void LateUpdate() 
    {
        Vector3 cameraPosition = transform.position; // aktuelle Position der Kamera

        // horizontale Positon wird neu berechnet
        // nimm den größeren der beiden Werte
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); 
        transform.position = cameraPosition; // wird angewendet
    }
}
