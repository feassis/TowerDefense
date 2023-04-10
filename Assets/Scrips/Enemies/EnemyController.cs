using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private bool isFlying;
    [SerializeField] private Transform aimPoint;

    private float speedModifier = 1f;

    private Path path;
    private int currentPathPoint = 0;
    private bool rechedEnd;
    private float attackTimer;

    private Castle castle;
    private Transform selectedAttackPoint;

    private bool isCastleDead;

    public Transform GetAimPoint() => aimPoint;

    private void Start()
    {
        attackTimer = 1 / fireRate;
    }

    public void SetSpeedModifier(float modifier)
    {
        speedModifier = modifier;
    }

    public void Setup(Path desiredPath, Castle desiredCastle)
    {
        path = desiredPath;
        castle = desiredCastle;
        castle.OnCasleDeath += HandleOnCastleDeath;
    }

    private void OnDestroy()
    {
        castle.OnCasleDeath += HandleOnCastleDeath;
    }

    private void HandleOnCastleDeath()
    {
        isCastleDead = true;
    }

    private void Update()
    {
        if (isCastleDead)
        {
            return;
        }

        if (rechedEnd)
        {
            if(selectedAttackPoint == null)
            {
                selectedAttackPoint = castle.GetRandomAttackPoint();
            }

            if(Vector3.Distance(transform.position, selectedAttackPoint.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position,
            selectedAttackPoint.position, moveSpeed *  speedModifier * Time.deltaTime);
            }
            
              attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackTimer = 1 / fireRate;

                castle.TakeDamage(damage);
            }
            return;
        }

        var myPathPoints = path.GetPathPoints();

        Transform targetPoint = isFlying ? myPathPoints[myPathPoints.Length - 1] : myPathPoints[currentPathPoint];

        transform.LookAt(targetPoint);

        transform.position = Vector3.MoveTowards(transform.position,
            targetPoint.position, moveSpeed *  speedModifier * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            currentPathPoint++;

            if(currentPathPoint >= myPathPoints.Length)
            {
                rechedEnd = true;
            }
        }

    }
}
