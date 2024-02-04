using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Vector3 movement;
    Animator animator;
    int isWalkingHash;
    int isJumpingHash;
    bool isGrounded;

    public float rotationSpeed=200;

    PlayerInputActions _input;
    Rigidbody _rigidbody;

    [SerializeField] float groundCheckDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isJumpingHash = Animator.StringToHash("isJumping");

        _input = new PlayerInputActions();
        _rigidbody = GetComponent<Rigidbody>();

        // Enable jump actions
        _input.PlayerActionMap.Enable();
        _input.PlayerActionMap.Jump.canceled += Jump_canceled;
        _input.PlayerActionMap.Jump.performed += Jump_performed;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is moving
        if (movement.magnitude > 0.1f)
        {
            // Calculate rotation based on movement direction
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);

            // Player is moving
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
            animator.SetBool(isWalkingHash, true);
        }
        else
        {
            // Player is not moving
            animator.SetBool(isWalkingHash, false);
        }

        // Perform ground check in Update
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, groundCheckDistance);
    }

    public void OnMovement(InputValue val)
    {
        SetMovement(val.Get<Vector2>());
        animator.SetBool(isWalkingHash, true);
    }

    private void SetMovement(Vector2 moveVal)
    {
        movement = new Vector3(moveVal.x, 0, moveVal.y);
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        // Check if the player is grounded before allowing the jump
        if (isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);

            // Set the animator parameter to true when jump is performed
            animator.SetBool(isJumpingHash, true);
        }
    }

    private void Jump_canceled(InputAction.CallbackContext context)
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