using UnityEngine;

public class RotatorObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 axis;

    private void Update()
    {
        target.Rotate(axis, rotationSpeed * Time.deltaTime * 60);
    }
}