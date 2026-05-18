using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class MovementStats : ScriptableObject
{
    [Header("Locomotion")]
    public float maxSpeed = 6f;
    public float acceleration = 30f;
    public float accelerationFactorFromDot = 1f;
    public float maxAccelForce = 40f;
    public float maxAccelerationForceFactorFromDot = 1f;
    public float forceScale = 1f;

    [Header("Gravity")]
    public float gravityScaleDrop = 2f;

    [Header("Jumping")]
    public float jumpUpVel = 8f;
    public float jumpBufferTime = 0.2f;           
    public float coyoteTimeThreshold = 0.15f;
    public float jumpHoldForce = 25f;
    public float jumpTerminalVelocity = -20f;
    public float jumpHoldDuration = 0.15f;

    public float fallGravityMultiplier = 2.5f;
    public float lowJumpGravityMultiplier = 4f;


    [Header("Context Actions")]
    public float landingContextThreshold = -10f;
    public float minLandingContextDuration = 0.2f;

    [Header("Hover Physics")]
    public float rideHeight = 1.2f; // How high you want to float, usually recommend 1 + your_character_hight/2
    public float rideSpringStrength = 250f; // How stiff the suspension is
    public float rideSpringDamper = 20f; // Prevents infinite bouncing
}