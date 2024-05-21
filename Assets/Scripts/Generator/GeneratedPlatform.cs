using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratedPlatform : MonoBehaviour
{
    [SerializeField] private List<GeneratedPlatform> availableNextPrefabs;
    [SerializeField] private Transform nextPlatformGenerationPoint;

    public Transform NextPlatformTransformPoint => nextPlatformGenerationPoint;
    public GeneratedPlatform GetRandomNext()
    {
        return availableNextPrefabs[Random.Range(0, availableNextPrefabs.Count)];
    }
    
    public void ShiftBack(float shiftBy)
    {
        transform.position += Vector3.back * shiftBy;
    }
}
