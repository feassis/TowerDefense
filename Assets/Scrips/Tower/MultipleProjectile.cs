using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleProjectile : Projectile
{
    [SerializeField] private List<Projectile> projectiles;
    
    public override void SetDamage(float damage)
    {
        foreach (var projectile in projectiles)
        {
            projectile.DamageAmount = damage;
        }
    }
}
