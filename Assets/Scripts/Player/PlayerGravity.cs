using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    PlayerLocomotion locomotion;
    PlayerGroundCheck ground; // 1. Add reference to your ground check
    MovementStats stats;

    float verticalVelocity;

    public void Initialize(MovementStats s)
    {
        stats = s;
    }

    void Awake()
    {
        locomotion = GetComponent<PlayerLocomotion>();
        ground = GetComponent<PlayerGroundCheck>(); // 2. Get the component
    }

    void Update()
    {
        // 3. Reset gravity if we are on the ground and falling
        if (ground.IsGrounded && verticalVelocity < 0)
        {
            // We set it to a small negative number (like -2f) instead of 0.
            // This forces the CharacterController to constantly press into the floor,
            // which helps it walk down slopes smoothly and keeps ground detection reliable.
            verticalVelocity = -2f;
        }

        verticalVelocity += Physics.gravity.y * stats.gravityScaleDrop * Time.deltaTime;

        if (verticalVelocity < stats.jumpTerminalVelocity)
            verticalVelocity = stats.jumpTerminalVelocity;

        locomotion.SetVerticalVelocity(verticalVelocity);
    }

    public void SetVelocity(float v)
    {
        verticalVelocity = v;
    }

    public float GetVelocity()
    {
        return verticalVelocity;
    }
}