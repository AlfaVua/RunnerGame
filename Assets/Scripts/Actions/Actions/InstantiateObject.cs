using UnityEngine;

public class InstantiateObject : ActionBase
{
    [SerializeField] private GameObject @object;
    public Transform targetContainer;
    public Transform positionTarget;
    public override void Execute()
    {
        Instantiate(@object, positionTarget.position, Quaternion.identity, targetContainer);
    }
}