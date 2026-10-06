using UnityEngine;
using UnityEngine.InputSystem;
public sealed class PlayerInputReader : MonoBehaviour
{
    [SerializeField]
    private InputActionReference moveAction;
    public Vector2 MoveInput { get; private set; }

    private void OnEnable()
    {
        moveAction.action.performed += OnMovePerformed;
        moveAction.action.canceled += OnMoveCanceled;

        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.performed -= OnMovePerformed;
        moveAction.action.canceled -= OnMoveCanceled;

        moveAction.action.Disable();
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }   
}