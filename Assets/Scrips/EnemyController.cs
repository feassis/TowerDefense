using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Path path;
    private int currentPathPoint = 0;
    private bool rechedEnd;

    private void Start()
    {
        path = FindObjectOfType<Path>();//change when enemy is spawned
    }



    private void Update()
    {
        if (rechedEnd)
        {
            return;
        }

        var myPathPoints = path.GetPathPoints();

        transform.LookAt(myPathPoints[currentPathPoint]);

        transform.position = Vector3.MoveTowards(transform.position, 
            myPathPoints[currentPathPoint].position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, myPathPoints[currentPathPoint].position) < 0.01f)
        {
            currentPathPoint++;

            if(currentPathPoint >= myPathPoints.Length)
            {
                rechedEnd = true;
            }
        }
    }
}
