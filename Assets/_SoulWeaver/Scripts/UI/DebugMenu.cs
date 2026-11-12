using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMenu : MonoBehaviour
{
    public static DebugMenu Instance;

    [Header("Input Reference")]
    [SerializeField] private InputActionReference toggleMenuAction;

    [Header("UI References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private PlayerCombat playerCombat;

    private bool isMenuOpen = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        // Habilitamos la acción para que pueda ser leída
        if (toggleMenuAction != null)
        {
            toggleMenuAction.action.Enable();
        }
    }

    private void Update()
    {
        // VERIFICACIÓN ESTABLE:
        // Consultamos directamente si la acción fue presionada en este frame
        if (toggleMenuAction != null && toggleMenuAction.action.WasPressedThisFrame())
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        panel.SetActive(isMenuOpen);

        if (isMenuOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void TestPlayerStun()
    {
        if (playerCombat != null)
        {
            playerCombat.GetStunned();
        }
    }
}
