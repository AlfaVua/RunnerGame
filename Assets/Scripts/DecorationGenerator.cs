using UnityEngine;

public class DecorationGenerator : GeneratorBase<DecorationWeightsManager, DecorationObject>
{
    [SerializeField] private Vector3 baseSize;
    [SerializeField] private Vector3 padding;
    protected override DecorationObject GenerateNewObject()
    {
        var prefab = objects.GetRandom();
        return Instantiate(prefab, container.transform.position + RandomLocalPosition(), prefab.transform.rotation, container);
    }

    private Vector3 RandomLocalPosition()
    {
        var scale = transform.localScale;
        return new Vector3(Random.Range(padding.x, scale.x - padding.x) * baseSize.x, 0, Random.Range(padding.z, scale.z - padding.z) * baseSize.z);
    }

    public void GenerateObjects(int amount)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < amount; i++) GenerateNewObject();
    }
}