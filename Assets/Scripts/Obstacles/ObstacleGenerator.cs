using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private ObstacleWeightsManager prefabs;
    [SerializeField] private List<Transform> generationPoints;

    private void Awake()
    {
        prefabs.EvaluateWeights();
    }

    public void GenerateNew()
    {
        var target = prefabs.GetRandom();
        if (target == null) return;
        var point = generationPoints[Random.Range(0, generationPoints.Count)];
        Instantiate(target, point);
    }
}