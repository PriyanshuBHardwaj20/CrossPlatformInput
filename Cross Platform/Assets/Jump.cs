using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    PlayerInputActions _input;
    Rigidbody _rigidbody;
    Animator animator;
    int isJumpingHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    private void Awake()
    {
        _input = new PlayerInputActions();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.PlayerActionMap.Enable();
        _input.PlayerActionMap.Jump.canceled += Jump_canceled;
        _input.PlayerActionMap.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);

        // Set the animator parameter to true when jump is performed
        animator.SetBool(isJumpingHash, true);
    }

    private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        var jumpOverTime = context.duration;
        if (jumpOverTime > 1)
        {
            return;
        }

        if (jumpOverTime > 0.2f)
        {
            _rigidbody.AddForce(Vector3.up * (5f * (float)jumpOverTime), ForceMode.Impulse);
        }

        // Set the animator parameter to false when jump is canceled
        animator.SetBool(isJumpingHash, false);
    }
}
