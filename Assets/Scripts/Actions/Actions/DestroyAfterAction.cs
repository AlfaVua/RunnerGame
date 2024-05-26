using UnityEngine;

public class DestroyAfterAction : ActionBase
{
    [SerializeField] private GameObject target;
    [SerializeField] private float time;
    
    public override void Execute()
    {
        Destroy(target, time);
    }
}