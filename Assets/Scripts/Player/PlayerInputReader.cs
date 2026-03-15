using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }

    void Update()
    {
        MoveInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        JumpPressed = Input.GetButtonDown("Jump");
    }

    public void ClearJump()
    {
        JumpPressed = false;
    }
}