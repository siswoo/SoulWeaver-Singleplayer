using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    [Header("Nivel y Experiencia")]
    public int level = 1;
    public float currentXP = 0;
    public float xpToNextLevel = 100;

    [Header("Atributos")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float attackDamage = 10f;

    // Evento para avisar a la UI que los stats cambiaron
    public event Action OnStatsChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void AddExperience(float amount)
    {
        currentXP += amount;
        Debug.Log($"XP ganada: {amount}. Total: {currentXP}/{xpToNextLevel}");

        // Verificar si subimos de nivel
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        OnStatsChanged?.Invoke(); // Avisamos a la UI
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;
        level++;

        // Aumentamos stats al subir de nivel (ejemplo)
        maxHealth += 20f;
        currentHealth = maxHealth; // Curamos al jugador al subir nivel
        attackDamage += 2f;

        // Aumentamos la dificultad para el siguiente nivel
        xpToNextLevel *= 1.2f;

        Debug.Log($"¡SUBISTE DE NIVEL! Ahora eres nivel {level}");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Debug.Log("El jugador ha muerto");
            // Aquí luego pondremos la pantalla de Game Over
        }
        OnStatsChanged?.Invoke();
    }
}
