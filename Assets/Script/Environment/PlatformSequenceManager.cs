using UnityEngine;
using System.Collections;

// Reihenfolge bei betreten der Blumen
// Progress wie weit der Spieler gekommen ist
// aktiviert/deaktiviert die Blumen
public class PlatformSequenceManager : MonoBehaviour
{
    public GameObject flower1;
    public GameObject flower2;
    public GameObject flower3;
    public GameObject flower4;
    public GameObject flower5;

    // interner Fortschritt: welche Blume wurde schon "betreten"
    private int progress = 0;

    private bool sequenceActive = false;
    private bool finalFlowerLockedIn = false; // für die finale Blume

    // wird vom Trigger aufgerufen -> StartFlowerSequenceTrigger.cs
    public void BeginSequence()
    {
        sequenceActive = true;

        // erstmal nur die erste Blume aktivieren
        flower1.SetActive(true);
        flower2.SetActive(false);
        flower3.SetActive(false);
        flower4.SetActive(false);
        flower5.SetActive(false);

        progress = 0; // Fortschritt zurücksetzen
        finalFlowerLockedIn = false;
    }

    // wird von jeder Plattform gemeldet, wenn Player drauf landet
    public void OnFlowerLanded(GameObject flower)
    {
        if (!sequenceActive)
            return;

        // Fall: Player landet auf Blume -> nächste Blume erscheint
        if (flower == flower1 && progress < 1)
        {
            progress = 1;
            flower2.SetActive(true);
        }
        else if (flower == flower2 && progress < 2)
        {
            progress = 2;
            flower3.SetActive(true);
        }
        else if (flower == flower3 && progress < 3)
        {
            progress = 3;
            flower4.SetActive(true);
        }
        // Player landet auf Flower4
        else if (flower == flower4 && progress < 4)
        {
            progress = 4;
            // extra Routine
            if (!finalFlowerLockedIn)
            {
                StartCoroutine(Flower5Routine());
            }
        }
        // letzte Blume
        else if (flower == flower5 && progress < 5)
        {
            progress = 5;
        }
    }

    // Speziallogik für die letze Blume
    private IEnumerator Flower5Routine()
    {
        if (finalFlowerLockedIn)
            yield break;

        flower5.SetActive(true); // kurz anzeigen

        yield return new WaitForSeconds(1f); 
        flower5.SetActive(false); // wieder ausschalten

        yield return new WaitForSeconds(3f);

        flower5.SetActive(true); // endgültig wieder anschalten
        finalFlowerLockedIn = true;
    }
}
