using System.Collections.Generic;
using UnityEngine;

public class GeneratorObject : MonoBehaviour
{
    [SerializeField] private GeneratedObject startingObject;
    [SerializeField] private Transform removingPoint;
    [SerializeField] public float startingShiftSpeed = 5;
    [SerializeField] private int maxGeneratedObjectAmount = 10;
    private List<GeneratedObject> _objectsList = new List<GeneratedObject>();
    private GeneratedObject _lastGeneratedObject;
    private float _currentShiftingSpeed;

    public void StartGenerator()
    {
        _currentShiftingSpeed = startingShiftSpeed;
        GenerateLevel();
    }

    private void GenerateNextObject()
    {
        var newPosition = _lastGeneratedObject.NextPlatformTransformPoint.position;
        var nextPrefab = _lastGeneratedObject.GetRandomNext();
        var randomNext = Instantiate(nextPrefab, newPosition, nextPrefab.transform.rotation, transform);
        _objectsList.Add(randomNext);
        _lastGeneratedObject = randomNext;
        if (_objectsList.Count > maxGeneratedObjectAmount)
        {
            RemoveLastObject();
        }
    }

    private void RemoveLastObject()
    {
        var removedObject = _objectsList[0];
        _objectsList.RemoveAt(0);
        Destroy(removedObject.gameObject);
    }

    private void GenerateLevel()
    {
        _lastGeneratedObject = startingObject;
        _objectsList.Add(_lastGeneratedObject);
        for (var i = 1; i < maxGeneratedObjectAmount; i++) GenerateNextObject();
    }

    private void Update()
    {
        _objectsList.ForEach(@object =>
        {
            @object.ShiftBack(_currentShiftingSpeed * Time.deltaTime);
        });
        if (_objectsList[0].transform.position.z > removingPoint.position.z) return;
        GenerateNextObject();
        _currentShiftingSpeed += .1f;
    }
}
