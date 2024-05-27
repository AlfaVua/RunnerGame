using UnityEngine;

public class MapObjectBase : MonoBehaviour
{
    [SerializeField] private float distanceToTheGround;
    [SerializeField] private LayerMask groundMask;

    private void Start()
    {
        SnapToTheGround();
    }

    private void SnapToTheGround()
    {
        var groundOffset = Vector3.up * distanceToTheGround;
        Physics.Raycast(transform.position + groundOffset + Vector3.up * 10, Vector3.down, out var hitInfo, float.MaxValue, groundMask);
        if (hitInfo.collider is null) return;
        transform.position = hitInfo.point + groundOffset;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanceToTheGround);
    }
}