using UnityEngine;
using UnityEngine.InputSystem; // <--- IMPORTANTE: Añadimos esta librería

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SetCursorState(false);
    }

    private void Update()
    {
        // FORMA CORRECTA EN EL NUEVO SISTEMA:
        // Verificamos si la tecla Escape o Tab fue presionada este frame
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ToggleCursor();
        }
    }

    public void ToggleCursor()
    {
        isPaused = !isPaused;
        SetCursorState(isPaused);
    }

    private void SetCursorState(bool showMouse)
    {
        if (showMouse)
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
}
