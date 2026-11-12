using UnityEngine;

public class AttackVolumeHandler : MonoBehaviour
{
    [SerializeField] private PlayerCombat combatScript;

    private void OnTriggerEnter(Collider other)
    {
        // El volumen detecta el choque y le avisa al script de combate del padre
        combatScript.OnAttackHit(other);
    }
}
