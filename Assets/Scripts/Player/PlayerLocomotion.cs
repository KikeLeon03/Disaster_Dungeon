using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerInputReader input;
    MovementStats stats;
    CharacterController controller;

    Vector3 velocity;

    void Awake()
    {
        input = GetComponent<PlayerInputReader>();
        controller = GetComponent<CharacterController>();
    }

    public void Initialize(MovementStats s)
    {
        stats = s;
    }

    void Update()
    {
        Vector3 moveDir = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);

        if (moveDir.magnitude > 1)
            moveDir.Normalize();

        Vector3 targetVelocity = moveDir * stats.maxSpeed;

        velocity.x = Mathf.MoveTowards(
            velocity.x,
            targetVelocity.x,
            stats.acceleration * Time.deltaTime
        );

        velocity.z = Mathf.MoveTowards(
            velocity.z,
            targetVelocity.z,
            stats.acceleration * Time.deltaTime
        );

        controller.Move(velocity * Time.deltaTime);
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void SetVerticalVelocity(float y)
    {
        velocity.y = y;
    }
}