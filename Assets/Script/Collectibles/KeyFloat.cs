using UnityEngine;

// auf und abschweben des SWchlüssels
public class KeyFloat : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatSpeed = 2f;   // Schnelligkeit der Bewegung
    public float floatAmount = 0.25f; // Stärke der Bewegung

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; // Ausgangsposition vom Key
    }

    private void Update()
    {
        // Berechnung mit Sinus Funktion
        float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatAmount;

        // Position anwenden
        transform.position = startPos + new Vector3(0, offsetY, 0);
    }
}
