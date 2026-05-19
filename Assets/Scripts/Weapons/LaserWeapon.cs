using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserWeapon : WeaponBase
{
    [SerializeField] private WeaponStats stats;

    private LineRenderer line;
    private float damageTickTimer;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.useWorldSpace = true;
        line.enabled = false;
    }

    public override void OnAttackHeld(Vector3 origin, Vector3 direction, float deltaTime)
    {
        line.enabled = true;

        Vector3 endPoint;
        if (Physics.Raycast(origin, direction, out RaycastHit hit, stats.range))
        {
            endPoint = hit.point;

            damageTickTimer -= deltaTime;
            if (damageTickTimer <= 0f)
            {
                damageTickTimer = stats.damageTick;
                hit.collider.GetComponent<IDamageable>()?.TakeDamage(stats.damage);
            }
        }
        else
        {
            endPoint = origin + direction * stats.range;
            damageTickTimer = 0f;
        }

        line.SetPosition(0, origin);
        line.SetPosition(1, endPoint);
    }

    // Lasers don't use single-press; reserved for other weapon types.
    public override void OnAttackTriggered(Vector3 origin, Vector3 direction) { }

    public override void OnAttackReleased()
    {
        line.enabled = false;
        damageTickTimer = 0f;
    }
}
