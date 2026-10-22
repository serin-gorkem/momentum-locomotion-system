using UnityEngine;

[CreateAssetMenu(
    fileName = "MovementConfig",
    menuName = "Momentum/Movement Config"
)]
public sealed class MovementConfig : ScriptableObject
{
    [SerializeField, Min(0f)]
    private float maxSpeed = 5f;

    [SerializeField, Min(0f)]
    private float acceleration = 20f;

    [SerializeField, Min(0f)]
    private float deceleration = 25f;

    [SerializeField]
    private float gravity = -25f;

    [SerializeField]
    private float groundedForce = -2f;

    [SerializeField, Min(0f)]
    private float jumpHeight = 2f;

    [SerializeField, Min(0f)]
    private float airAcceleration = 8f;
    public float Gravity => gravity;
    public float GroundedForce => groundedForce;
    public float MaxSpeed => maxSpeed;
    public float Acceleration => acceleration;
    public float Deceleration => deceleration;
    public float JumpHeight => jumpHeight;
    public float AirAcceleration => airAcceleration;
}