using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    // Optional override for the fire origin (e.g. a hand bone or gun muzzle).
    // Falls back to player position + eye height when null.
    [SerializeField] private Transform aimOrigin;

    private PlayerInputReader input;
    private WeaponBase activeWeapon;
    private bool wasAttackHeld;

    void Awake()
    {
        input        = GetComponent<PlayerInputReader>();
        activeWeapon = GetComponentInChildren<WeaponBase>();
    }

    // Accepts MovementStats to stay consistent with the rest of the player system.
    // Extend this if weapons ever need movement data (e.g. recoil, velocity-based spread).
    public void Initialize(MovementStats stats) { }

    void Update()
    {
        bool held = input.AttackHeld;

        if (input.AttackTriggered)
        {
            activeWeapon?.OnAttackTriggered(AimOrigin(), AimDirection());
            input.ClearAttack();
        }

        if (held)
            activeWeapon?.OnAttackHeld(AimOrigin(), AimDirection(), Time.deltaTime);
        else if (wasAttackHeld)
            activeWeapon?.OnAttackReleased();

        wasAttackHeld = held;
    }

    private Vector3 AimOrigin() =>
        aimOrigin != null ? aimOrigin.position : transform.position + Vector3.up * 1f;

    private Vector3 AimDirection() =>
        mainCamera != null ? mainCamera.forward : transform.forward;
}
