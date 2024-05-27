using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct MinMax<T>
{
    [SerializeField] private T min;
    [SerializeField] private T max;
    public T Min => min;
    public T Max => max;
}

[Serializable]
public struct MinMaxVector3
{
    [SerializeField] private Vector3 min;
    [SerializeField] private Vector3 max;
    public Vector3 Min => min;
    public Vector3 Max => max;

    public Vector3 RandomInBounds => new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
}