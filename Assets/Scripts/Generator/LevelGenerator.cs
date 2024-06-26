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
    [SerializeField] private Transform losingPoint;
    [SerializeField] private MapObjectsGenerator objectGenerator;
    private List<GeneratedPlatform> _platformList = new List<GeneratedPlatform>();
    private GeneratedPlatform _lastGeneratedPlatform;

    public float ShiftingSpeed { get; private set; }

    public void StartGenerator()
    {
        ClearLevel();
        objectGenerator.Init();
        ShiftingSpeed = startingShiftSpeed;
        GenerateLevel();
    }

    private void ClearLevel()
    {
        _platformList.Clear();
        objectGenerator.Clear();
        foreach (Transform child in levelContainer)
        {
            child.gameObject.SetActive(false); // prevent incorrect collision when generating level
            Destroy(child.gameObject);
        }
    }

    private void GenerateLevel()
    {
        _lastGeneratedPlatform = Instantiate(startingPlatform, levelContainer);
        _platformList.Add(_lastGeneratedPlatform);
        for (var i = 1; i < maxGeneratedObjectAmount; i++) GenerateNextPlatform();
    }

    private void GenerateNextPlatform()
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
        losingPoint.transform.position = Vector3.up * (FindLowestPlatformY() - 5);
        objectGenerator.OnPlatformGenerated(randomNext);
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
        var shiftBy = ShiftingSpeed * Time.deltaTime;
        _platformList.ForEach(platform => platform.ShiftBack(shiftBy));
        objectGenerator.UpdateObjectsPosition(shiftBy, removingPoint.position.z);
        
        if (_platformList[0].transform.position.z > removingPoint.position.z) return;
        GenerateNextPlatform();
        ShiftingSpeed += .075f;
    }
}
