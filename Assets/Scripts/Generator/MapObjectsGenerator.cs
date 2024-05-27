using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class MapObjectsGenerator : GeneratorBase<MapObjectWeightsManager, MapObjectBase>
{
    [SerializeField] private List<Transform> playerMovingLines;
    private Vector3 _lastGeneratedPlatformPosition;
    public void OnPlatformGenerated(GeneratedPlatform platform)
    {
        _lastGeneratedPlatformPosition = platform.transform.position;
    }

    protected override MapObjectBase GenerateNewObject()
    {
        var targetLinePosition = playerMovingLines[Random.Range(0, playerMovingLines.Count)].position;
        var targetPosition = new Vector3(targetLinePosition.x, _lastGeneratedPlatformPosition.y, _lastGeneratedPlatformPosition.z);
        return GenerateObjectAtPosition(targetPosition);
    }

    private MapObjectBase GenerateObjectAtPosition(Vector3 position)
    {
        var prefab = objects.GetRandom();
        return Instantiate(prefab, position, prefab.transform.rotation, container);
    }

    public void Clear()
    {
        CurrentIntervalTime = 0;
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}