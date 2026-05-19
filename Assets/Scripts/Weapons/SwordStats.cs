using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Sword")]
public class SwordStats : ScriptableObject
{
    [Header("Damage")]
    public float damage = 20f;

    [Header("Hitbox")]
    public float attackRadius = 1.5f;
    public float attackOffset = 1.2f; // distance in front of origin

    [Header("Combo")]
    public int maxComboSteps = 3;
    public float comboWindow = 0.7f;  // seconds to chain the next hit
}
