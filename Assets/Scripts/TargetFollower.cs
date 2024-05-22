using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 followSpeed;
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private void FixedUpdate()
    {
        var positionDifference = target.position - transform.position;
        transform.position += new Vector3(
            positionDifference.x * followSpeed.x,
            positionDifference.y * followSpeed.y,
            positionDifference.z * followSpeed.z
            ) * (60 * Time.fixedDeltaTime);
    }
}
