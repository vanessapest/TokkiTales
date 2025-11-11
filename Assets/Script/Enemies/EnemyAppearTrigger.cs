using UnityEngine;

// Enemy erscheint, wenn Spieler bestimmten Bereich betritt
public class EnemyAppearTrigger : MonoBehaviour
{
    public GameObject enemy; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.SetActive(true); 
            Debug.Log("Enemy appeared!");
        }
    }
}
