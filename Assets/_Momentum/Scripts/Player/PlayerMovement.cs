using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInputReader inputReader;
    [SerializeField] PlayerMotor playerMotor;
    [SerializeField]
    private MovementConfig config;

    // Runtime state
    private Vector3 horizontalVelocity;
    private float verticalVelocity;

    void Update()
    {
        Vector2 input = inputReader.moveInput;

        Vector3 direction = new Vector3(
            input.x,
            0f,
            input.y
        );
        Vector3 clampedDirection = Vector3.ClampMagnitude(direction, 1f);
        Vector3 targetVelocity = clampedDirection * config.MaxSpeed;

        bool hasMovementInput = direction.sqrMagnitude > 0f;
        float velocityChangeRate =
            hasMovementInput
            ? config.Acceleration
            : config.Deceleration;

        horizontalVelocity = Vector3.MoveTowards(
            horizontalVelocity,
            targetVelocity,
            velocityChangeRate * Time.deltaTime
        );

        if (playerMotor.IsGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = config.GroundedForce;
        }

        if (inputReader.JumpPressedThisFrame && playerMotor.IsGrounded)
        {
            verticalVelocity = Mathf.Sqrt(
             config.JumpHeight *
             -2f *
             config.Gravity
            );
        }
        
        verticalVelocity += config.Gravity * Time.deltaTime;

        Vector3 velocity = horizontalVelocity + Vector3.up * verticalVelocity;

        Vector3 displacement = velocity * Time.deltaTime;

        playerMotor.Move(displacement);
        Debug.Log($"Grounded: {playerMotor.IsGrounded}");
    }
}
