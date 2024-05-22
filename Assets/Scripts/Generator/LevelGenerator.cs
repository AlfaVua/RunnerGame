using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GeneratedPlatform startingPlatform;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private Transform removingPoint;
    [SerializeField] public float startingShiftSpeed = 7;
    [SerializeField] private int maxGeneratedObjectAmount = 10;
    [SerializeField] private Transform loosingPoint;
    private List<GeneratedPlatform> _platformList = new List<GeneratedPlatform>();
    private GeneratedPlatform _lastGeneratedPlatform;

    public float ShiftingSpeed { get; private set; }

    public void StartGenerator()
    {
        ClearLevel();
        ShiftingSpeed = startingShiftSpeed;
        GenerateLevel();
    }

    private void ClearLevel()
    {
        _platformList.Clear();
        foreach (Transform child in levelContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void GenerateLevel()
    {
        _lastGeneratedPlatform = Instantiate(startingPlatform, levelContainer);
        _platformList.Add(_lastGeneratedPlatform);
        for (var i = 1; i < maxGeneratedObjectAmount; i++) GenerateNextObject();
    }

    private void GenerateNextObject()
    {
        var newPosition = _lastGeneratedPlatform.NextPlatformTransformPoint.position;
        var nextPrefab = _lastGeneratedPlatform.GetRandomNext();
        var randomNext = Instantiate(nextPrefab, newPosition, nextPrefab.transform.rotation, levelContainer);
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

    private void Update()
    {
        if (_platformList.Count == 0) return;
        _platformList.ForEach(platform =>
        {
            platform.ShiftBack(ShiftingSpeed * Time.deltaTime);
        });
        if (_platformList[0].transform.position.z > removingPoint.position.z) return;
        GenerateNextObject();
        ShiftingSpeed += .075f;
    }
}
