using UnityEngine;

public class OffsetPoint : MonoBehaviour
{
    public Transform character;   // The cylinder
    public float rightOffset = 1f;
    public float upOffset = 1f;

    void Update()
    {
        transform.position =
            character.position +
            character.right * rightOffset +
            character.up * upOffset;
    }
}