using UnityEngine;

public class RotateRandom : ActionBase
{
    [SerializeField] private Transform target;
    public override void Execute()
    {
        target.transform.rotation = Random.rotation;
    }
}