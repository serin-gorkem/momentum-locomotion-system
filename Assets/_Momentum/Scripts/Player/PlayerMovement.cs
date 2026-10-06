using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInputReader inputReader;
    [SerializeField] PlayerMotor playerMotor;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 25f;

    //Runtime STATE thats why its not SerializeField.
    private Vector3 currentVelocity;

    void Update()
    {
        Vector2 input = inputReader.MoveInput;

        Vector3 direction = new Vector3(
            input.x,
            0f,
            input.y
        );
        Vector3 clampedDirection = Vector3.ClampMagnitude(direction, 1f);
        Vector3 targetVelocity = clampedDirection * maxSpeed;

        bool hasMovementInput = direction.sqrMagnitude > 0f;
        float velocityChangeRate =
            hasMovementInput
            ? acceleration
            : deceleration;

        currentVelocity = Vector3.MoveTowards(
            currentVelocity,
            targetVelocity,
            velocityChangeRate * Time.deltaTime
        );

        Vector3 displacement = currentVelocity * Time.deltaTime;

        playerMotor.Move(displacement);

    }
}
