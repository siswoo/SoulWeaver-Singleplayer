using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image healthFill;
    [SerializeField] private Health healthScript;
    [SerializeField] private CanvasGroup canvasGroup; // <-- NUEVO: Referencia al CanvasGroup

    [Header("Settings")]
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float displayDuration = 3f;

    private float targetFill = 1f;
    private Transform mainCamera;
    private Coroutine hideRoutine;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
        if (healthScript == null) healthScript = GetComponentInParent<Health>();

        // Si no asignaste el CanvasGroup, lo busca automáticamente
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

        // Empezamos invisibles
        canvasGroup.alpha = 0f;
    }

    private void Update()
    {
        // Ahora esto SIEMPRE funciona, aunque la barra sea invisible
        transform.LookAt(transform.position + mainCamera.rotation * Vector3.forward,
                         mainCamera.rotation * Vector3.up);

        targetFill = healthScript.CurrentHealth / healthScript.GetMaxHealth();

        healthFill.color = Color.Lerp(Color.red, Color.green, targetFill);
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, targetFill, Time.deltaTime * lerpSpeed);
    }

    public void Show()
    {
        // En lugar de SetActive, cambiamos el alpha a 1 (visible)
        canvasGroup.alpha = 1f;

        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        // En lugar de SetActive, cambiamos el alpha a 0 (invisible)
        canvasGroup.alpha = 0f;
    }
}
