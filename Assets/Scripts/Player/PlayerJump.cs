using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerInputReader input;
    PlayerGroundCheck ground;
    PlayerGravity gravity;
    MovementStats stats;

    float coyoteTimer;

    void Awake()
    {
        input = GetComponent<PlayerInputReader>();
        ground = GetComponent<PlayerGroundCheck>();
        gravity = GetComponent<PlayerGravity>();
    }

    public void Initialize(MovementStats s)
    {
        stats = s;
    }

    void Update()
    {
        if (ground.IsGrounded)
            coyoteTimer = stats.coyoteTimeThreshold;
        else
            coyoteTimer -= Time.deltaTime;

        if (input.JumpPressed && coyoteTimer > 0)
        {
            Jump();
            input.ClearJump();
        }
    }

    void Jump()
    {
        gravity.SetVelocity(stats.jumpUpVel);
        coyoteTimer = 0;
    }
}