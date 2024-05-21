using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratedObject : MonoBehaviour
{
    [SerializeField] private List<GeneratedObject> availableNextPrefabs;
    [SerializeField] private Transform nextPlatformGenerationPoint;

    public Transform NextPlatformTransformPoint => nextPlatformGenerationPoint;
    public GeneratedObject GetRandomNext()
    {
        return availableNextPrefabs[Random.Range(0, availableNextPrefabs.Count)];
    }
    
    public void ShiftBack(float shiftBy)
    {
        transform.position += Vector3.back * shiftBy;
    }
}
