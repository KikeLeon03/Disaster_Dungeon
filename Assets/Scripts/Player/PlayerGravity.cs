using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerGravity : MonoBehaviour
{
    PlayerGroundCheck ground;
    PlayerInputReader input; 
    MovementStats stats;
    Rigidbody rb;

    public void Initialize(MovementStats s)
    {
        stats = s;
    }

    void Awake()
    {
        ground = GetComponent<PlayerGroundCheck>();
        input = GetComponent<PlayerInputReader>(); 
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float currentYVel = rb.linearVelocity.y;
        float gravityMultiplier = stats.gravityScaleDrop;

        rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);

        // Terminal velocity
        if (rb.linearVelocity.y < stats.jumpTerminalVelocity)
        {
            Vector3 vel = rb.linearVelocity;
            vel.y = stats.jumpTerminalVelocity;
            rb.linearVelocity = vel;
        }
    }
}