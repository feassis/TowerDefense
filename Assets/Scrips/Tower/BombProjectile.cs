using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movementDuration = 1f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float damage = 40f;
    [SerializeField] private float explosionRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

    private Transform target;

    public void SetDamage(float newDamage) => damage = newDamage;

    private Vector3 targetPoint;

    private void Start()
    {
        ServiceLocator.GetService<AudioManager>().PlaySFX(SFXEnum.BombFire);
        rb.velocity = ((target.position - transform.position) 
            - Physics.gravity * movementDuration * movementDuration * 0.5f)/ movementDuration;
    }

    public void SetTarget(Transform desiredTarget)
    {
        target = desiredTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] enemiesColliders = Physics.OverlapSphere(transform.position, explosionRange, enemyLayer);

        foreach (var enemy in enemiesColliders)
        {
            if(enemy.TryGetComponent<EnemyHealth>(out EnemyHealth health))
            {
                health.TakeDamage(damage);
            }
        }

        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        ServiceLocator.GetService<AudioManager>().PlaySFX(SFXEnum.BombExplode);
        Destroy(gameObject);
    }
}
