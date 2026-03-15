using UnityEngine;


public class SpherecastSpring : MonoBehaviour
{
    [Header("Spring Settings")]
    [SerializeField] public bool onlyPushAway = true;
    [SerializeField] public float stiffness = 150f;
    [SerializeField] public float damper = 15f;
    [SerializeField] public float neutralForceDistance = 2f; // Distance to the bounce layer for which the spring force is 0

    [Header("Raycast Settings")]
    [SerializeField] public float raycastLength = 5f; // How far down to look for the ground
    [SerializeField] public LayerMask bounceLayer; // Make sure to set this in the Inspector!

    // NEW: Radius for the SphereCast. Match this to your character's collider radius!
    [SerializeField] public float sphereRadius = 0.5f;

    [SerializeField] private Rigidbody rb = null;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 1. Fire a SPHERECAST downwards to find the ground
        if (Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out RaycastHit hit, raycastLength, bounceLayer))
        {
            Vector3 desiredPosition = hit.point + (Vector3.up * neutralForceDistance);

            if (!onlyPushAway | (transform.position.y < desiredPosition.y))
            {
                // 2. ISOLATE Y-AXIS DISPLACEMENT
                // We only care about the vertical height difference, zeroing out X and Z.
                float yDisplacement = transform.position.y - desiredPosition.y;
                Vector3 displacement = new Vector3(0f, yDisplacement, 0f);

                // 3. ISOLATE Y-AXIS VELOCITY
                // We only dampen vertical falling/bouncing. Damping X and Z would create unwanted drag!
                Vector3 velocity = new Vector3(0f, rb.linearVelocity.y, 0f);

                // 4. Calculate the Spring Force using your static method
                Vector3 springForce = SpringPhysics.HookesLaw(displacement, velocity, stiffness, damper);

                // 5. Apply the force to the Rigidbody (Now strictly vertical)
                rb.AddForce(springForce);
            }
        }
    }
}