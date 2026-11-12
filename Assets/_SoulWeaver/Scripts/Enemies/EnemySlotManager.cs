using UnityEngine;

public class EnemySlotManager : MonoBehaviour
{
    [Header("Configuración de Enemigos")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Slots de Posición")]
    // Arrastra aquí tus 3 Transform vacíos desde el Inspector
    [SerializeField] private Transform[] spawnSlots;

    // Lista interna para rastrear qué enemigo está en qué slot
    private GameObject[] activeEnemies;

    private void Awake()
    {
        // Inicializamos el array de seguimiento basándonos en la cantidad de slots configurados
        activeEnemies = new GameObject[spawnSlots.Length];
    }

    /// <summary>
    /// Función pública para intentar spawnear un enemigo en el primer slot disponible.
    /// </summary>
    public void TrySpawnEnemy()
    {
        int emptySlotIndex = GetFirstEmptySlot();

        if (emptySlotIndex != -1)
        {
            SpawnEnemyInSlot(emptySlotIndex);
        }
        else
        {
            Debug.Log("Slots llenos: No se puede spawnear más enemigos.");
        }
    }

    private int GetFirstEmptySlot()
    {
        for (int i = 0; i < spawnSlots.Length; i++)
        {
            // El slot está vacío si:
            // 1. Nunca se ha spawneado nada ahí (null)
            // 2. El enemigo fue destruido (null)
            // 3. El enemigo fue desactivado por el script Health (activeInHierarchy == false)
            if (activeEnemies[i] == null || !activeEnemies[i].activeInHierarchy)
            {
                return i;
            }
        }
        return -1; // Todos los slots están ocupados
    }

    private void SpawnEnemyInSlot(int index)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnSlots[index].position, spawnSlots[index].rotation);

        // Registramos el enemigo en el slot correspondiente
        activeEnemies[index] = newEnemy;

        Debug.Log($"Enemigo creado en Slot {index}");
    }
}
