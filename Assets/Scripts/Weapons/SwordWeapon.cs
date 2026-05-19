using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : WeaponBase
{
    [SerializeField] private SwordStats stats;
    [SerializeField] private LayerMask hitLayers = ~0; // default: all layers

    private int comboStep;
    private float comboResetTimer;
    private readonly HashSet<Collider> hitThisSwing = new HashSet<Collider>();

    void Update()
    {
        if (comboResetTimer > 0f)
        {
            comboResetTimer -= Time.deltaTime;
            if (comboResetTimer <= 0f)
                comboStep = 0;
        }
    }

    public override void OnAttackTriggered(Vector3 origin, Vector3 direction)
    {
        comboStep = comboStep >= stats.maxComboSteps ? 1 : comboStep + 1;
        comboResetTimer = stats.comboWindow;
        hitThisSwing.Clear();

        Vector3 hitCenter = origin + direction * stats.attackOffset;
        Collider[] hits = Physics.OverlapSphere(hitCenter, stats.attackRadius, hitLayers);

        foreach (Collider col in hits)
        {
            if (!hitThisSwing.Add(col)) continue;
            col.GetComponent<IDamageable>()?.TakeDamage(stats.damage);
            Debug.Log($"[Sword] Combo {comboStep}/{stats.maxComboSteps} hit {col.name}");
        }
    }

    // Reserved for charged attacks or sustained slashes in future weapons.
    public override void OnAttackHeld(Vector3 origin, Vector3 direction, float deltaTime) { }
    public override void OnAttackReleased() { }

    void OnDrawGizmosSelected()
    {
        // Visualise the attack range in the Scene view
        Gizmos.color = new Color(1f, 0.4f, 0f, 0.35f);
        Gizmos.DrawSphere(transform.position + transform.forward * stats.attackOffset,
                          stats.attackRadius);
    }
}
