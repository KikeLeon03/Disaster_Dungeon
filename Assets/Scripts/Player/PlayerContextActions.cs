using System.Diagnostics;
using UnityEngine;

public class PlayerContextActions : MonoBehaviour
{
    PlayerGravity gravity;
    PlayerGroundCheck ground;
    MovementStats stats;

    bool wasGrounded;

    void Awake()
    {
        gravity = GetComponent<PlayerGravity>();
        ground = GetComponent<PlayerGroundCheck>();
    }

    public void Initialize(MovementStats s)
    {
        stats = s;
    }

    void Update()
    {
        if (!wasGrounded && ground.IsGrounded)
        {
            float impact = gravity.GetVelocity();

            if (impact < stats.landingContextThreshold)
            {
                UnityEngine.Debug.Log("Hard Landing!");
            }
        }

        wasGrounded = ground.IsGrounded;
    }
}