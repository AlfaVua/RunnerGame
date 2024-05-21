using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPositionLinePlatform : GeneratedPlatform
{
    [SerializeField] private List<Vector3> points;
    [SerializeField] private Transform target;

    private void Awake()
    {
        target.localPosition = points[Random.Range(0, points.Count)];
    }

    private void OnDrawGizmos()
    {
        var origin = transform.position;
        Gizmos.color = Color.cyan;
        points.ForEach(point => Gizmos.DrawSphere(point + origin, .2f));
    }
}