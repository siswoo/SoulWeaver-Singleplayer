using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform playerCamera;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float rotateDampening = 0.1f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 3.0f; // Bajado un poco para realismo
    [SerializeField] private float gravity = -20f; // Gravedad más fuerte para que no flote

    private float turnSmoothingVelocity;
    private float verticalVelocity;

    private void Update()
    {
        // 1. Calculamos el movimiento horizontal
        Vector3 horizontalMove = CalculateMovement();

        // 2. Calculamos la gravedad y el salto
        float vVelocity = CalculateVerticalVelocity();

        // 3. COMBINAMOS TODO en un solo vector final
        Vector3 finalMovement = horizontalMove + (Vector3.up * vVelocity);

        // 4. UN SOLO MOVIMIENTO por frame (Esto arregla el isGrounded)
        controller.Move(finalMovement * Time.deltaTime);
    }

    private Vector3 CalculateMovement()
    {
        Vector2 input = playerInput.MoveInput;
        Vector3 playerDirection = new Vector3(input.x, 0f, input.y).normalized;

        if (playerDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(playerDirection.x, playerDirection.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float smoothTargetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothingVelocity, rotateDampening);

            transform.rotation = Quaternion.Euler(0f, smoothTargetAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            return moveDirection.normalized * walkSpeed;
        }
        return Vector3.zero;
    }

    private float CalculateVerticalVelocity()
    {
        if (controller.isGrounded)
        {
            // Valor negativo pequeño para mantener el personaje pegado al suelo
            if (verticalVelocity < 0) verticalVelocity = -2f;

            if (playerInput.JumpPressed)
            {
                // Fórmula física real para el salto: v = sqrt(h * -2 * g)
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        return verticalVelocity;
    }
}
