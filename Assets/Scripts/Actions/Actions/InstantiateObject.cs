using UnityEngine;

public class InstantiateObject : ActionBase
{
    [SerializeField] private GameObject @object;
    public Transform targetContainer;
    public Vector3 position;
    public override void Execute()
    {
        Instantiate(@object, position, Quaternion.identity, targetContainer);
    }
}