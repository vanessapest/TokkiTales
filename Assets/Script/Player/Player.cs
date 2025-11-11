using UnityEngine;

// regelt, was passiert, wenn der Spieler getroffen oder besiegt wird
public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer spriteRenderer;
    private DeathAnimation deathAnimation;

    // um zu prüfen, ob der Spieler lebt oder tot ist
    public bool sprite => spriteRenderer.enabled;
    public bool dead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }

    public void Hit() // wird aufgerufen, wenn der Spieler getroffen wird -> Enemy.cs
    {
        Death();
    }

    private void Death()
    {
        spriteRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(1.5f);
    }
}
