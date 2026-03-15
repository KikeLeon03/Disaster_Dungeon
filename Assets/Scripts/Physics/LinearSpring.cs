using UnityEngine;

public class SpringPhysics
{
    public static Vector3 HookesLaw(Vector3 displacement, Vector3 velocity, float stiffness, float damper)
    {
        Vector3 force = (stiffness * displacement) + (damper * velocity);
        force = -force;
        return force;
    }
}

public class LinearSpring : MonoBehaviour
{
    [Header("Spring Settings")]
    [SerializeField] public bool onlyPushAway = true;
    [SerializeField] public float stiffness = 150f;
    [SerializeField] public float damper = 15f;
    [SerializeField] public float neutralForceDistance = 2f; // Distance to the bounce layer for which the spring force is 0

    [Header("Raycast Settings")]
    [SerializeField] public float raycastLength = 5f; // How far down to look for the ground
    [SerializeField] public LayerMask bounceLayer; // Make sure to set this in the Inspector!

    [SerializeField] private Rigidbody rb = null;

    

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 1. Fire a raycast downwards to find the ground
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, raycastLength, bounceLayer))
        {
            Vector3 desiredPosition = hit.point + (Vector3.up * neutralForceDistance);
            if (!onlyPushAway | (transform.position.y < desiredPosition.y))
            {

                // 2. Calculate Displacement (Current Position - Desired Position)

                Vector3 displacement = transform.position - desiredPosition;

                // 3. Get Velocity (Since the ground is stationary, we just use the Rigidbody's velocity)
                Vector3 velocity = rb.linearVelocity;

                // 4. Calculate the Spring Force using your static method
                Vector3 springForce = SpringPhysics.HookesLaw(displacement, velocity, stiffness, damper);

                // 5. Apply the force to the Rigidbody
                rb.AddForce(springForce);
            }
        }
    }
}