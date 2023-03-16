using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;

    public Transform[] GetPathPoints() => pathPoints;
}
