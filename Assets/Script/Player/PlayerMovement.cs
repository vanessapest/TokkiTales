using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private Vector2 velocity; // eigene Geschwindigkeitsberechnung
    private float inputAxis; // wie stark der Spieler auf die Tasten drückt

    public float moveSpeed = 1f;
    public float maxJumpHeight = 0.2f;
    public float maxJumpTime = 0.5f;

    public AudioClip jumpSound;

    // arrow syntax: turning it into a property, computing this based on other values
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f); // half the time -> multiply and dividing by time
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    // properties that have a public getter, but a private setter -> need to read the value in other scripts
    public bool grounded { get; private set; } 
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f); // turning movement 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HorizontalMovement();

        // Extensions.cs -> prüft, ob direkt unter dem Spieler Boden ist
        grounded = rb.Raycast(Vector2.down, ignoreTriggers: true); 

        if (grounded)
        {
            GroundedMovement();
        }

        ApplyGravity();
    }

    private void HorizontalMovement() // can still move in the air
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime * 3f); // applied our acceleration

        if (rb.Raycast(Vector2.right * velocity.x, ignoreTriggers: true)) // checked if we're running to a wall
        {
            velocity.x = 0f;
        }

        // set player's rotation
        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero; // by default sprite is already facing to the right
        }
        else if (velocity.x < 0f) // sprite change when velocity changes -> not when pressing the button
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement() // specifically only when you're grounded, detecting if you should jump
    {
        velocity.y = Mathf.Max(velocity.y, 0f); // restrict the velocity from building up 
        jumping = velocity.y > 0f; // we know you're jumping if that velocity is greater than 0

        if (Input.GetButtonDown("Jump")) // check if you press Jump Input
        {
            velocity.y = jumpForce;
            jumping = true;

            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f;  
        float multiplier = falling ? 2f : 1f;
        // if you're falling -> applying twice the gravity or if you let go of the jump input -> apply extra gravity 
        // gravity is going to kick in even stronger right away -> gets a shorter jump

        velocity.y += gravity * multiplier * Time.deltaTime; // gravity applied over time
        velocity.y = Mathf.Max(velocity.y, gravity / 2f); // prevent the player from falling too fast
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = mainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f); // Spieler darf nicht aus der Kamera herauslaufen

        rb.MovePosition(position);
    }
    
    private void OnCollisionEnter2D(Collision2D collision) // dependent on the object you're colliding with
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Key")) // to check if the object the player collides with is above the player
        {
            if (transform.DotTest(collision.transform, Vector2.up)) // collided with this object moving upwards
            {
                velocity.y = 0f;
            }
        }
    }
}
