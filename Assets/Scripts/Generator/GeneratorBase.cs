using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class GeneratorBase<T, TO> : MonoBehaviour where T : WeightsManager<TO> where TO : MonoBehaviour
{
    [SerializeField] protected T objects;
    [SerializeField] protected Transform container;
    [SerializeField] protected MinMax<float> generationInterval;
    [SerializeField][Range(0.00001f, 1)] protected float generationChance = 1;

    private readonly List<TO> _generatedObjects = new List<TO>();
    protected float CurrentIntervalTime = 0;
    private float _nextGenerationInterval;

    public void Init()
    {
        UpdateNextInterval();
    }
    
    public void UpdateObjectsPosition(float shiftBy, float? removingPointZ = null)
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
        CurrentIntervalTime += value;
        if (CurrentIntervalTime < _nextGenerationInterval) return;
        if (Random.value <= generationChance)
        {
            var obj = GenerateNewObject();
            if (!obj) return;
            _generatedObjects.Add(obj);
        }
        UpdateNextInterval();
        CurrentIntervalTime = 0;
    }

    private void UpdateNextInterval()
    {
        _nextGenerationInterval = Random.Range(generationInterval.Min, generationInterval.Max);
    }

    protected abstract TO GenerateNewObject();
}