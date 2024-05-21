using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private TargetFollower lineFollower;
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

    private void Jump(InputAction.CallbackContext context)
    {
        rigidBody.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    private void OnLineChanged()
    {
        lineFollower.SetTarget(_linesPositions[_targetLineIndex]);
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
