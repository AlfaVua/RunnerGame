using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GeneratedPlatform startingPlatform;
    [SerializeField] private Transform removingPoint;
    [SerializeField] public float startingShiftSpeed = 7;
    [SerializeField] private int maxGeneratedObjectAmount = 10;
    [SerializeField] private Transform loosingPoint;
    private List<GeneratedPlatform> _platformList = new List<GeneratedPlatform>();
    private GeneratedPlatform _lastGeneratedPlatform;
    private float _currentShiftingSpeed;

    public float ShiftingSpeed => _currentShiftingSpeed;

    public void StartGenerator()
    {
        _currentShiftingSpeed = startingShiftSpeed;
        GenerateLevel();
    }

    private void GenerateNextObject()
    {
        var newPosition = _lastGeneratedPlatform.NextPlatformTransformPoint.position;
        var nextPrefab = _lastGeneratedPlatform.GetRandomNext();
        var randomNext = Instantiate(nextPrefab, newPosition, nextPrefab.transform.rotation, transform);
        _platformList.Add(randomNext);
        _lastGeneratedPlatform = randomNext;
        if (_platformList.Count > maxGeneratedObjectAmount)
        {
            RemoveLastObject();
        }
        loosingPoint.transform.position = Vector3.up * (FindLowestPlatformY() - 5);
    }

    private void RemoveLastObject()
    {
        var removedObject = _platformList[0];
        _platformList.RemoveAt(0);
        Destroy(removedObject.gameObject);
    }

    private float FindLowestPlatformY()
    {
        return _platformList.Min(platform => platform.transform.position.y);
    }

    private void GenerateLevel()
    {
        _lastGeneratedPlatform = startingPlatform;
        _platformList.Add(_lastGeneratedPlatform);
        for (var i = 1; i < maxGeneratedObjectAmount; i++) GenerateNextObject();
    }

    private void Update()
    {
        if (_platformList.Count == 0) return;
        _platformList.ForEach(platform =>
        {
            platform.ShiftBack(_currentShiftingSpeed * Time.deltaTime);
        });
        if (_platformList[0].transform.position.z > removingPoint.position.z) return;
        GenerateNextObject();
        _currentShiftingSpeed += .075f;
    }
}
