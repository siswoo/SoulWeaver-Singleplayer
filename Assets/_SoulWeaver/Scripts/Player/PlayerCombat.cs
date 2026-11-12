using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject attackVolume;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Animator animator;

    [Header("Combat Settings")]
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float knockbackForce = 2.5f;
    [SerializeField] private string attackTriggerName = "Attack";
    [SerializeField] private string attackBoolName = "isAttacking";
    [SerializeField] private string hitTriggerName = "Hit";

    private bool isAttacking = false;
    private Vector3 originalVolumePos;

    private void Awake()
    {
        if (attackVolume != null) originalVolumePos = attackVolume.transform.localPosition;
    }

    private void Update()
    {
        if (playerInput != null && playerInput.AttackPressed && !isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        // 1. Avisamos al Animator que empezamos a atacar
        if (animator != null)
        {
            animator.SetTrigger(attackTriggerName);
            animator.SetBool(attackBoolName, true);
        }

        if (attackVolume != null)
        {
            attackVolume.SetActive(true);
            Vector3 targetPos = originalVolumePos + (Vector3.forward * attackRange);
            float elapsed = 0f;

            while (elapsed < attackDuration)
            {
                if (attackVolume != null)
                {
                    attackVolume.transform.localPosition = Vector3.Lerp(originalVolumePos, targetPos, elapsed / attackDuration);
                }
                elapsed += Time.deltaTime;
                yield return null;
            }

            attackVolume.SetActive(false);
            if (attackVolume != null) attackVolume.transform.localPosition = originalVolumePos;
        }

        // 2. IMPORTANTE: Solo volvemos a Idle si no fuimos interrumpidos
        if (isAttacking)
        {
            EndAttack();
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
        if (animator != null)
        {
            animator.SetBool(attackBoolName, false);
        }
    }

    // ESTO ES LO QUE BUSCABAS: La función de interrupción
    public void GetStunned()
    {
        isAttacking = false; // Cortamos la lógica del ataque
        if (animator != null)
        {
            animator.SetTrigger(hitTriggerName); // Disparamos la animación de daño
            animator.SetBool(attackBoolName, false); // Forzamos la salida del estado de ataque
        }
        Debug.Log("¡El jugador ha sido interrumpido!");
    }

    public void OnAttackHit(Collider other)
    {
        Health enemyHealth = other.GetComponentInParent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(25f);
            EnemyHitEffect hitEffect = other.GetComponentInParent<EnemyHitEffect>();
            if (hitEffect != null) hitEffect.PlayHitEffect();

            EnemyKnockback knockback = other.GetComponentInParent<EnemyKnockback>();
            if (knockback != null)
            {
                Vector3 pushDir = (other.transform.position - transform.position).normalized;
                pushDir.y = 0;
                knockback.ApplyKnockback(pushDir, knockbackForce, 0.2f);
            }

            if (GameFeelManager.Instance != null) GameFeelManager.Instance.HitStop(0.05f);
            if (attackVolume != null)
            {
                var impulseSource = attackVolume.GetComponent<CinemachineImpulseSource>();
                if (impulseSource != null) impulseSource.GenerateImpulse();
            }
        }
    }
}
