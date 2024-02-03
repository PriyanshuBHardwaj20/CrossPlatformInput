using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Vector3 movement;
    Animator animator;
    int isWalkingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is moving
        if (movement.magnitude > 0.1f)
        {
            // Player is moving
            transform.Translate(speed * Time.deltaTime * movement);
            animator.SetBool(isWalkingHash, true);
        }
        else
        {
            // Player is not moving
            animator.SetBool(isWalkingHash, false);
        }
    }

    public void OnMovement(InputValue val)
    {
        SetMovement(val.Get<Vector2>());
        animator.SetBool(isWalkingHash, true);
    }

    private void SetMovement(Vector2 moveVal)
    {
        movement = moveVal;
        movement.z = movement.y;
        movement.y = 0;
    }
}