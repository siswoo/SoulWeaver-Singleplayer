using UnityEngine;
using System.Collections;

public class EnemyKnockback : MonoBehaviour
{
    private bool isBeingKnockedBack = false;

    public void ApplyKnockback(Vector3 knockbackDirection, float force, float duration)
    {
        if (isBeingKnockedBack) return;
        StartCoroutine(KnockbackRoutine(knockbackDirection, force, duration));
    }

    private IEnumerator KnockbackRoutine(Vector3 direction, float force, float duration)
    {
        isBeingKnockedBack = true;

        float elapsed = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + (direction * force);

        while (elapsed < duration)
        {
            // Movimiento suave hacia atrás
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isBeingKnockedBack = false;
    }
}
