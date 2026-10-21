using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerInputReader : MonoBehaviour
{
    [SerializeField] InputActionReference moveAction;
    public Vector2 moveInput { get; private set; }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

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
}