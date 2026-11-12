using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] private GameObject soulPrefab;

    private void Awake()
    {
        // Buscamos el componente Health en el mismo objeto y nos "suscribimos" al evento de muerte
        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.OnDeath += DropSoul; // "Cuando mueras, ejecuta la función DropSoul"
        }
    }

    private void DropSoul()
    {
        if (soulPrefab != null)
        {
            Instantiate(soulPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
