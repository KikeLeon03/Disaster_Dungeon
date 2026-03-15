using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] public MovementStats stats;

    void Awake()
    {
        GetComponent<PlayerLocomotion>().Initialize(stats);
        GetComponent<PlayerGravity>().Initialize(stats);
        GetComponent<PlayerJump>().Initialize(stats);
        GetComponent<PlayerContextActions>().Initialize(stats);
    }
}