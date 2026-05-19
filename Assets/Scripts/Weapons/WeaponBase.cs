using UnityEngine;

// All weapons implement this contract.
// origin: world-space fire point  |  direction: normalised aim direction
public abstract class WeaponBase : MonoBehaviour
{
    public abstract void OnAttackHeld(Vector3 origin, Vector3 direction, float deltaTime);
    public abstract void OnAttackTriggered(Vector3 origin, Vector3 direction);
    public abstract void OnAttackReleased();
}
