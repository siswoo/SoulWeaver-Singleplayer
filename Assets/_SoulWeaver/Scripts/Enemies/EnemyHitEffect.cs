using UnityEngine;
using System.Collections;

public class EnemyHitEffect : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color hitColor = Color.white;
    private Color originalColor;

    private void Awake()
    {
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    public void PlayHitEffect()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        meshRenderer.material.color = hitColor; // Cambia a blanco
        yield return new WaitForSeconds(0.1f); // Espera un instante
        meshRenderer.material.color = originalColor; // Vuelve al original
    }
}
