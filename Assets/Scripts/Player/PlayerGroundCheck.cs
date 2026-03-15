using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        IsGrounded = controller.isGrounded;
    }
}