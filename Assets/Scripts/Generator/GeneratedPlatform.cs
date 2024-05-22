using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratedPlatform : MonoBehaviour
{
    [SerializeField] private List<GeneratedPlatform> availableNextPrefabs;
    [SerializeField] private Transform nextPlatformGenerationPoint;
    [SerializeField] private ObstacleGenerator obstacleGenerator;

    public Transform NextPlatformTransformPoint => nextPlatformGenerationPoint;

    private void Awake()
    {
        obstacleGenerator?.GenerateNew();
    }

    public GeneratedPlatform GetRandomNext()
    {
        return availableNextPrefabs[Random.Range(0, availableNextPrefabs.Count)];
    }
    
    public void ShiftBack(float shiftBy)
    {
        transform.position += Vector3.back * shiftBy;
    }
}
