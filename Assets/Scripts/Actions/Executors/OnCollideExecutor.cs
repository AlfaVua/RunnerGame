using UnityEngine;

public class OnCollideExecutor : ActionExecutorBase
{
    [SerializeField] private bool onCollide;
    [SerializeField] private bool onTrigger;
    private void OnCollisionEnter(Collision other)
    {
        if (onCollide)
            Execute();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onTrigger)
            Execute();
    }
}