using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class MapObjectsGenerator : MonoBehaviour
{
    [SerializeField] private MapObjectWeightsManager objects;
    [SerializeField] private List<Transform> playerMovingLines;
    [SerializeField] private Transform container;
    [SerializeField] private MinMax<float> generationInterval;
    [SerializeField][Range(0.00001f, 1)] private float generationChance = 1;

    private readonly List<MapObjectBase> _generatedObjects = new List<MapObjectBase>();
    private float _currentIntervalTime = 0;
    private Vector3 _lastGeneratedPlatformPosition;
    private float _nextGenerationInterval;

    public void Init()
    {
        UpdateNextInterval();
    }
    public void OnPlatformGenerated(GeneratedPlatform platform)
    {
        _lastGeneratedPlatformPosition = platform.transform.position;
    }

    private void GenerateNewObject()
    {
        var targetLinePosition = playerMovingLines[Random.Range(0, playerMovingLines.Count)].position;
        var targetPosition = new Vector3(targetLinePosition.x, _lastGeneratedPlatformPosition.y, _lastGeneratedPlatformPosition.z);
        GenerateObjectAtPosition(targetPosition);
    }

    private void GenerateObjectAtPosition(Vector3 position)
    {
        var prefab = objects.GetRandom();
        var objectPosition = prefab.GetPosition(position);
        if (objectPosition == Vector3.zero) return;
        var obj = Instantiate(prefab, objectPosition, prefab.transform.rotation, container);
        _generatedObjects.Add(obj);
    }

    public void UpdateObjectsPosition(float shiftBy, float removingPointZ)
    {
        UpdateInterval(shiftBy);
        for (var i = 0; i < _generatedObjects.Count; i++)
        {
            var generatedObject = _generatedObjects[i];
            if (!generatedObject)
            {
                _generatedObjects.Remove(generatedObject);
                i--;
                continue;
            }
            generatedObject.transform.position += Vector3.back * shiftBy;
            if (generatedObject.transform.position.z > removingPointZ) continue;
            Destroy(generatedObject.gameObject);
        }
    }

    private void UpdateInterval(float value)
    {
        _currentIntervalTime += value;
        if (_currentIntervalTime < _nextGenerationInterval) return;
        if (Random.value <= generationChance) GenerateNewObject();
        UpdateNextInterval();
        _currentIntervalTime = 0;
    }

    private void UpdateNextInterval()
    {
        _nextGenerationInterval = Random.Range(generationInterval.Min, generationInterval.Max);
    }

    public void Clear()
    {
        _currentIntervalTime = 0;
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}