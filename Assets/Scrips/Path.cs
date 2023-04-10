using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private Castle myCastle;

    public Castle GetMyCastle() => myCastle;

    public Transform[] GetPathPoints() => pathPoints;
}
