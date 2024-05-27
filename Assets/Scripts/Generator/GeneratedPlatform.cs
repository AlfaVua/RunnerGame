using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratedPlatform : MonoBehaviour
{
    [SerializeField] private List<GeneratedPlatform> availableNextPrefabs;
    [SerializeField] private Transform nextPlatformGenerationPoint;
    [SerializeField] [CanBeNull] private ObstacleGenerator obstacleGenerator;
    [SerializeField] private List<DecorationGenerator> decorationGenerators;

    public Transform NextPlatformTransformPoint => nextPlatformGenerationPoint;

    private void Awake()
    {
        obstacleGenerator?.GenerateNew();
        decorationGenerators.ForEach(generator => generator.GenerateObjects(Random.Range(5, 20)));
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
