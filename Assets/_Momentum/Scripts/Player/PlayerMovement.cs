using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInputReader inputReader;
    [SerializeField] PlayerMotor playerMotor;
    [SerializeField]
    private MovementConfig config;

    //Runtime STATE thats why its not SerializeField.
    private Vector3 currentVelocity;

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

        currentVelocity = Vector3.MoveTowards(
            currentVelocity,
            targetVelocity,
            velocityChangeRate * Time.deltaTime
        );

        Vector3 displacement = currentVelocity * Time.deltaTime;

        playerMotor.Move(displacement);
    }
}
