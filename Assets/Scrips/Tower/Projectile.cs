using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody myRB;
    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject hitEffect;
    [SerializeField] private bool hitOnlyOneEnemy = true;

    public float DamageAmount = 10;
    private bool hitedEnemy = false; 

    private void Start()
    {
        myRB.velocity = transform.forward * moveSpeed;
        ServiceLocator.GetService<AudioManager>().PlaySFX(SFXEnum.CannonFire);
        StartCoroutine(DestroyAfter10s());
    }

    private IEnumerator DestroyAfter10s()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !hitedEnemy)
        {
            if(hitOnlyOneEnemy)
            {
                hitedEnemy = true;
            }
            other.GetComponent<EnemyHealth>().TakeDamage(DamageAmount);
        }

        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public virtual void SetDamage(float damage)
    {
        DamageAmount = damage;
    }
}
