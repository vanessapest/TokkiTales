using UnityEngine;

// prüft, ob ein Rigidbody2D vor sich ein Hindernis hat
// prüft, aus welcher Richtung zwei Objekte zueinander stehen (z. B. ob der Spieler von oben auf den Gegner trifft)

// statische Klasse, damit sie von überall aus erreichbar ist
public static class Extensions
{
    private const int DefaultLayerMask = 1 << 0; // 0 = Default

    // prüft, ob der Rigidbody in einer bestimmten Richtung auf ein Hindernis trifft
    // für Spielfiguren, ob sie Boden berühren oder nicht
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction, bool ignoreTriggers = true)
    {
        // kinematische Körper reagieren nicht auf Physik, also überspringt man sie hier
        if (rigidbody.bodyType == RigidbodyType2D.Kinematic)
            return false;

        float radius = 0.1f;
        float distance = 0.3f;

        RaycastHit2D hit = Physics2D.CircleCast( // Raycast aber in Kreisform
            rigidbody.position,
            radius,
            direction.normalized,
            distance,
            DefaultLayerMask
        );

        if (hit.collider == null) // wenn kein Objekt getroffen wurde
            return false;

        if (hit.rigidbody == rigidbody) // wenn man sich selbst trifft -> ignorieren
            return false;

        // Trigger-Collider nicht mitrechnen
        if (ignoreTriggers && hit.collider.isTrigger)
            return false;

        return true;
    }

    // prüft, aus welcher Richtung ein anderes Objekt kommt
    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position; // direction pointing from other to transform
        // other is object player is colliding with; transform is our player
        return Vector2.Dot(direction.normalized, testDirection) > 0.1f;
        // if dot product is greater than 0.1 that means you mostly are kind of in the same direction
    }
}
