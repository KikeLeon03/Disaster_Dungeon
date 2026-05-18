using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerInputReader : MonoBehaviour
{

    [Header("Input Actions")]
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference attackAction;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; } 
    
    public bool JumpTriggered { get; private set; } 
    public bool JumpHeld => jumpAction.action.IsPressed();

    public bool AttackTriggered { get; private set; }
    public bool AttackHeld => attackAction != null ? attackAction.action.IsPressed() : false;

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
            JumpTriggered = true;
    }

    private void OnMove(InputValue value) => MoveInput = value.Get<Vector2>();
    private void OnAttack(InputValue value)
    {
        if (value.isPressed)
            AttackTriggered = true;
    }

    public void ClearJump() => JumpTriggered = false;
    public void ClearAttack() => AttackTriggered = false;

    // Optional safety
    private void OnEnable()
    {
        jumpAction?.action?.Enable();
        attackAction?.action?.Enable();
    }
}