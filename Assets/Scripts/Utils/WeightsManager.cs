using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public abstract class WeightsManager<T> : ScriptableObject
    {
        [SerializeField] private List<WeightData<T>> weights;
        private float _totalWeightValue;

        public void EvaluateWeights()
        {
            if (weights == null || weights.Count == 0) return;
            weights.Sort((weight1, weight2) => (int)(weight1.weight - weight2.weight));
            _totalWeightValue = weights.Sum(weightData => weightData.weight);
        }

        public T GetRandom()
        {
            var targetWeight = Random.Range(0, _totalWeightValue);
            var currentWeight = 0f;
            foreach (var data in weights)
            {
                if ((currentWeight += data.weight) >= targetWeight) return data.data;
            }

            return weights[0].data;
        }
    }

    [Serializable]
    public struct WeightData<T>
    {
        public float weight;
        public T data;
    }
}