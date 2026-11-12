using UnityEngine;

public class SoulItem : MonoBehaviour
{
    [SerializeField] private float xpValue = 25f; // Cuánta XP da este alma

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si quien tocó el alma es el jugador
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponentInParent<PlayerStats>();
            if (stats != null)
            {
                stats.AddExperience(xpValue);
                Destroy(gameObject); // El alma desaparece al ser recogida
            }
        }
    }
}
