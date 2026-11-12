using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Efectos Visuales")]
    [SerializeField] private GameObject damageTextPrefab; // <-- NUEVO: Arrastra aquí el prefab del Damage Pop-Up

    [SerializeField] private HealthBar healthBar;

    public float CurrentHealth => currentHealth;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Vida restante: {currentHealth}");

        if (healthBar != null)
        {
            healthBar.Show();
        }

        // --- NUEVO: Lógica para mostrar el número de daño ---
        if (damageTextPrefab != null)
        {
            // Creamos el texto justo encima del enemigo (transform.position + un pequeño offset en Y)
            GameObject textObj = Instantiate(damageTextPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);

            // Obtenemos el componente FloatingText y le pasamos la cantidad de daño
            FloatingText ft = textObj.GetComponent<FloatingText>();
            if (ft != null)
            {
                ft.Setup(amount);
            }
        }
        // --------------------------------------------------

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }


}
