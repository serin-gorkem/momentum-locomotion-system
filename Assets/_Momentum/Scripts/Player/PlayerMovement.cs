using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInputReader inputReader;
    [SerializeField] PlayerMotor playerMotor;
    [SerializeField] private MovementConfig config;
    // Runtime state
    private Vector3 horizontalVelocity;
    private float verticalVelocity;
    public PlayerMovementState CurrentState { get; private set; }

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
        if (playerMotor.IsGrounded)
        {
            float groundChangeRate =
                hasMovementInput
                    ? config.Acceleration
                    : config.Deceleration;

            horizontalVelocity = Vector3.MoveTowards(
                horizontalVelocity,
                targetVelocity,
                groundChangeRate * Time.deltaTime
            );
        }
        else if (hasMovementInput)
        {
            horizontalVelocity = Vector3.MoveTowards(
                horizontalVelocity,
                targetVelocity,
                config.AirAcceleration * Time.deltaTime
            );
        }

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
        UpdateMovementState();
    }

    private void UpdateMovementState()
    {
        if (playerMotor.IsGrounded)
        {
            CurrentState = PlayerMovementState.Grounded;
        }
        else if (!playerMotor.IsGrounded && verticalVelocity > 0)
        {
            CurrentState = PlayerMovementState.Rising;
        }
        else
        {
            CurrentState = PlayerMovementState.Falling;
        }
    }
}