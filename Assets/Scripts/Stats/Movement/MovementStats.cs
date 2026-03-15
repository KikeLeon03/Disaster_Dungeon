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
    public float jumpUpVelFactorFromExistingY = 1f;
    public float analogJumpUpForce = 1f;
    public float jumpTerminalVelocity = -20f;
    public float jumpDuration = 0.2f;
    public float coyoteTimeThreshold = 0.15f;
    public float autoJumpAfterLandThreshold = 0.1f;
    public float jumpFallFactor = 1.5f;
    public float jumpSkipGroundCheckDuration = 0.05f;

    [Header("Context Actions")]
    public float landingContextThreshold = -10f;
    public float minLandingContextDuration = 0.2f;
}