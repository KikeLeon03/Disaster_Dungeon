using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Weapon")]
public class WeaponStats : ScriptableObject
{
    [Header("Damage")]
    public float damage = 5f;
    public float damageTick = 0.1f; // seconds between damage applications

    [Header("Range")]
    public float range = 25f;
}
