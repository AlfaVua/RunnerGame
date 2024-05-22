using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private TargetFollower lineFollower;
    [SerializeField] private float groundRaycastDistance = 1.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 collisionRayOffset;
    private PlayerControls _controls;
    private int _targetLineIndex;
    private List<Transform> _linesPositions;
    private void Awake()
    {
        lineFollower.enabled = false;
        _controls = new PlayerControls();
        _controls.Player.MoveLeft.performed += TryTurnLeft;
        _controls.Player.MoveRight.performed += TryTurnRight;
        _controls.Player.Jump.performed += Jump;
    }

    public void Init(List<Transform> lines)
    {
        _linesPositions = lines;
        _targetLineIndex = lines.Count / 2;
        lineFollower.enabled = true;
        OnLineChanged();
    }

    private void TryTurnLeft(InputAction.CallbackContext context)
    {
        if (_targetLineIndex == 0) return;
        _targetLineIndex--;
        OnLineChanged();
    }

    private void TryTurnRight(InputAction.CallbackContext context)
    {
        if (_targetLineIndex == _linesPositions.Count - 1) return;
        _targetLineIndex++;
        OnLineChanged();
    }

    private void OnLineChanged()
    {
        lineFollower.SetTarget(_linesPositions[_targetLineIndex]);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!Physics.Raycast(transform.position + collisionRayOffset, Vector3.down, groundRaycastDistance, groundLayer))
        {
            SpeedUpFalling();
            return;
        }
        var velocity = rigidBody.velocity;
        rigidBody.velocity = new Vector3(velocity.x, 0, velocity.z);
        rigidBody.AddForce(Vector3.up * 8, ForceMode.Impulse);
    }

    private void SpeedUpFalling()
    {
        rigidBody.AddForce(Vector3.down * 10, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Physics.gravity * Time.fixedDeltaTime;
        }
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void OnDrawGizmos()
    {
        var origin = transform.position + collisionRayOffset;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + Vector3.down * groundRaycastDistance);
    }
}
