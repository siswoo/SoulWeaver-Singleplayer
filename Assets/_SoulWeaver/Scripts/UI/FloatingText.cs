using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textMesh;

    [Header("Animation Curves")]
    public AnimationCurve moveCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve opacityCurve;

    [Header("Settings")]
    [SerializeField] private Vector3 moveDirection = new Vector3(0, 1, 0);
    [SerializeField] private float moveDistance = 1.5f;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Color startColor = Color.yellow;
    [SerializeField] private Color endColor = Color.white;

    private Coroutine activeRoutine;
    private Transform mainCameraTransform;

    private void Awake()
    {
        // Guardamos la referencia a la cámara para que sea más rápido que llamar a Camera.main cada frame
        if (Camera.main != null)
            mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // --- SOLUCIÓN AL BUG DE LA CÁMARA (BILLBOARDING) ---
        if (mainCameraTransform != null)
        {
            // Hace que el objeto siempre mire hacia la cámara
            transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
                            mainCameraTransform.rotation * Vector3.up);
        }
    }

    public void Setup(float damageAmount)
    {
        textMesh.text = $"-{damageAmount}";

        if (activeRoutine != null) StopCoroutine(activeRoutine);
        activeRoutine = StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        Vector3 startPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / animationDuration;

            // 1. MOVIMIENTO
            float curveValue = moveCurve.Evaluate(normalizedTime);
            transform.localPosition = startPos + (moveDirection * curveValue * moveDistance);

            // 2. ESCALA
            float scaleValue = scaleCurve.Evaluate(normalizedTime);
            transform.localScale = Vector3.one * scaleValue;

            // 3. COLOR Y OPACIDAD
            float opacityValue = opacityCurve.Evaluate(normalizedTime);
            Color currentColor = Color.Lerp(startColor, endColor, normalizedTime);
            currentColor.a = opacityValue;
            textMesh.color = currentColor;

            yield return null;
        }

        // --- SOLUCIÓN AL BUG DE DESAPARICIÓN ---
        // Es fundamental usar Destroy para que el objeto desaparezca totalmente de la escena
        Destroy(gameObject);
    }
}
