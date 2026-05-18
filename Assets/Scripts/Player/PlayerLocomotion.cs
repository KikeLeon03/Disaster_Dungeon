using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerLocomotion : MonoBehaviour
{
    PlayerInputReader input;
    PlayerGroundCheck ground;
    MovementStats stats;
    Rigidbody rb;

    [SerializeField] private float rotationSpeed = 15f; 
    [SerializeField] private bool inverseCameraMovement = false;
    
    // NEW: Reference to the camera so we know which way we are looking
    [SerializeField] private Transform mainCamera;

    void Awake()
    {
        input = GetComponent<PlayerInputReader>();
        ground = GetComponent<PlayerGroundCheck>();
        rb = GetComponent<Rigidbody>();
        
        rb.useGravity = false;
        rb.freezeRotation = true;
    }

    void Start()
    {
        // If you didn't assign a camera in the inspector, grab the default Main Camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }
    }

    public void Initialize(MovementStats s)
    {
        stats = s;
    }

    void FixedUpdate()
    {
        HandleHover();
        
        // Calculate our camera-relative input exactly once per frame
        Vector3 moveDir = GetCameraRelativeMoveDirection();
        
        // Pass it to both methods
        HandleMovement(moveDir);

        if (!inverseCameraMovement)
        {
            moveDir = -moveDir;
        }
        //HandleRotation(moveDir); 
    }

    // NEW METHOD: Converts raw input into camera-relative direction
    // private Vector3 GetCameraRelativeMoveDirection()
    // {
    //     return mainCamera.forward * input.MoveInput.y + mainCamera.right * input.MoveInput.x;
    // }
    private Vector3 GetCameraRelativeMoveDirection()
    {
        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraRight = mainCamera.right;

        // Remove vertical component
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize so diagonal movement isn't faster
        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * input.MoveInput.y +
            cameraRight * input.MoveInput.x;
    }

    private void HandleHover()
    {
        if (ground.IsGrounded)
        {
            float rayDirVelocity = Vector3.Dot(Vector3.down, rb.linearVelocity);
            float offset = ground.GroundDistance - stats.rideHeight;
            float springForce = (offset * stats.rideSpringStrength) - (rayDirVelocity * stats.rideSpringDamper);
            rb.AddForce(Vector3.down * springForce);
        }
    }

    // UPDATED: Now takes the pre-calculated moveDir
    private void HandleMovement(Vector3 moveDir)
    {
        moveDir.y = 0;
        Vector3 targetVelocity = moveDir * stats.maxSpeed;
        
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 currentXZVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);

        Vector3 velocityDifference = targetVelocity - currentXZVelocity;

        rb.AddForce(velocityDifference * stats.acceleration, ForceMode.Acceleration);
    }

    // UPDATED: Now takes the pre-calculated moveDir
    private void HandleRotation(Vector3 moveDir)
    {
        moveDir.y = 0; 
        transform.LookAt(transform.position + moveDir);
    }
}