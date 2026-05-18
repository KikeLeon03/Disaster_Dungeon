using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private Transform groundCheck; // Usually placed at the base of the player
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float castLength = 2f;
    
    [Tooltip("Should be slightly smaller than your character's actual collider radius to avoid scraping walls.")]
    [SerializeField] private float sphereRadius = 0.4f; 

    public bool IsGrounded { get; private set; }
    public float GroundDistance { get; private set; }
    public RaycastHit GroundHit;

    void FixedUpdate()
    {
        // We start the cast slightly ABOVE the bottom of the player. 
        // If we start it exactly at the bottom, and the player is resting on the floor, 
        // the sphere might already be intersecting the ground, which can cause the cast to fail or return distance 0.
        Vector3 origin = groundCheck.position + (Vector3.up * sphereRadius);
        
        IsGrounded = Physics.SphereCast(
            origin, 
            sphereRadius, 
            Vector3.down, 
            out GroundHit, 
            castLength, 
            groundLayer
        );
        
        if (IsGrounded)
        {
            // Because we moved the origin up by 'sphereRadius', the distance returned 
            // by the SphereCast perfectly matches the distance from our groundCheck transform to the floor.
            GroundDistance = GroundHit.distance; 
        }
        else
        {
            GroundDistance = castLength;
        }
        //UnityEngine.Debug.Log($"Ground Check: IsGrounded={IsGrounded}, GroundDistance={GroundDistance}");
    }

    // Updated Gizmos so you can actually see the "thickness" of your ground check
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            
            Vector3 origin = groundCheck.position + (Vector3.up * sphereRadius);
            Vector3 endPos = origin + Vector3.down * (IsGrounded ? GroundHit.distance : castLength);
            
            // Draw the starting sphere, ending sphere, and the path between them
            Gizmos.DrawWireSphere(origin, sphereRadius);
            Gizmos.DrawWireSphere(endPos, sphereRadius);
            Gizmos.DrawLine(origin + Vector3.left * sphereRadius, endPos + Vector3.left * sphereRadius);
            Gizmos.DrawLine(origin + Vector3.right * sphereRadius, endPos + Vector3.right * sphereRadius);
        }
    }
}