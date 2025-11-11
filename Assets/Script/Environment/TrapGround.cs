using System.Collections;
using UnityEngine;

// steuert den Fallboden (im letzen Part)
public class TrapGround : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Bodenteil
    private Collider2D col;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // wenn Spieler Bereich betritt -> TrapTrigger.cs gestartet
    public void StartTrapSequence()
    {
        StartCoroutine(TrapRoutine());
    }

    private IEnumerator TrapRoutine()
    {
        // erster Drop –> 3 Sekunden weg
        SetActiveState(false);
        yield return new WaitForSeconds(3f);

        // kurz sichtbar (0.5 Sek)
        SetActiveState(true);
        yield return new WaitForSeconds(1f);

        // nochmal weg für 3 Sekunden
        SetActiveState(false);
        yield return new WaitForSeconds(3f);

        // dauerhaft wieder da
        SetActiveState(true);
    }

    // Boden und Collider ein-/ausschalten
    private void SetActiveState(bool state)
    {
        spriteRenderer.enabled = state;
        col.enabled = state;
    }
}
