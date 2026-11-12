using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputActionAsset InputActions;

    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }

    private InputAction moveAction;
    private InputAction jumpAction;

    private void Awake()
    {
        var playerMap = InputActions.FindActionMap("Player");
        moveAction = playerMap.FindAction("Move");
        jumpAction = playerMap.FindAction("Jump");
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    private void Update()
    {
        MoveInput = moveAction.ReadValue<Vector2>();
        JumpPressed = jumpAction.WasPressedThisFrame();

        AttackPressed = Mouse.current.leftButton.wasPressedThisFrame;
    }

}
