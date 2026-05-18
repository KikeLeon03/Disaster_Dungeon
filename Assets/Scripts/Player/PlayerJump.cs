using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    private PlayerInputReader input;
    private PlayerGroundCheck ground;
    private Rigidbody rb;
    MovementStats stats;


    [Header("Debug")]
    [SerializeField] private bool debugLogs = false;

    // --- State ---
    private bool isJumping = false;
    private bool jumpConsumed = false;
    private float jumpBufferTimer = 0f;
    private float coyoteTimer = 0f;
    private bool wasGroundedLastFrame = false;
    private float jumpHoldTimer = 0f;

    void Awake()
    {
        input  = GetComponent<PlayerInputReader>();
        ground = GetComponent<PlayerGroundCheck>();
        rb     = GetComponent<Rigidbody>();
    }

    public void Initialize(MovementStats s)
    {
        stats = s;
    }

    // Update handles timers so they stay frame-independent
    void Update()
    {
        // Jump buffer: register the intent even if we're mid-air
        if (input.JumpTriggered)
        {
            jumpBufferTimer = stats.jumpBufferTime;
            input.ClearJump();
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        // Coyote time: keep a grace window after leaving a ledge
        if (ground.IsGrounded)
        {
            coyoteTimer          = stats.coyoteTimeThreshold;
            wasGroundedLastFrame = true;
        }
        else
        {
            wasGroundedLastFrame = false;
            coyoteTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        // --- Reset on landing ---
        if (ground.IsGrounded && rb.linearVelocity.y <= 0f)
        {
            jumpConsumed  = false;
            isJumping     = false;
            jumpHoldTimer = 0f;
        }

        bool canJump     = (ground.IsGrounded || coyoteTimer > 0f) && !jumpConsumed;
        bool wantsToJump = jumpBufferTimer > 0f;

        // --- Initiate jump ---
        if (wantsToJump && canJump)
        {
            jumpConsumed    = true;
            isJumping       = true;
            jumpHoldTimer   = 0f;
            jumpBufferTimer = 0f;
            coyoteTimer     = 0f;

            // Cancel any downward velocity so jumpUpVel is always a clean baseline,
            // regardless of how fast the player was falling before.
            Vector3 vel = rb.linearVelocity;
            vel.y = 0f;
            rb.linearVelocity = vel;

            // VelocityChange ignores mass — jumpUpVel directly becomes the Y velocity.
            rb.AddForce(Vector3.up * stats.jumpUpVel, ForceMode.VelocityChange);
            if (debugLogs)
                Debug.Log("Jump!");}

        // --- Hold phase ---
        // PlayerGravity is already pulling the player down every FixedUpdate.
        // This upward force fights that gravity while the button is held,
        // extending the arc. Tapering to zero at the end of jumpHoldDuration
        // makes the cutoff smooth rather than a hard stop.

        if (isJumping && input.JumpHeld && jumpHoldTimer < stats.jumpHoldDuration)
        {               
            jumpHoldTimer += Time.fixedDeltaTime;

            float holdProgress = 1f - (jumpHoldTimer / stats.jumpHoldDuration);
            rb.AddForce(Vector3.up * stats.jumpHoldForce * holdProgress, ForceMode.Acceleration);
        }
        else if (!input.JumpHeld || jumpHoldTimer >= stats.jumpHoldDuration)
        {
            isJumping = false;
        }
    }
}