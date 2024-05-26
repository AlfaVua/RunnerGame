using UnityEngine;

public class MapObjectBase : MonoBehaviour
{
    [SerializeField] private float distanceToTheGround;
    [SerializeField] private LayerMask groundMask;

    public float DistanceToTheGround => distanceToTheGround;

    public Vector3 GetPosition(Vector3 point)
    {
        var groundOffset = Vector3.up * distanceToTheGround;
        Physics.Raycast(point + groundOffset + Vector3.up, Vector3.down, out var hitInfo, float.MaxValue, groundMask);
        if (hitInfo.collider is null) return Vector3.zero;
        return hitInfo.point + groundOffset;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanceToTheGround);
    }
}