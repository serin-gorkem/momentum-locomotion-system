using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public sealed class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    public bool IsGrounded => characterController.isGrounded;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    public void Move(Vector3 displacement)
    {
        characterController.Move(displacement);
    }
}