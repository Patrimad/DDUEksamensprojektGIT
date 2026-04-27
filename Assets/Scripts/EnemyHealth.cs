using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    public int maxHealth = 3;
    private int currentHealth;

    public LayerMask DartLayer; // Layer for the enemy

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy died!");
            Die();
        }

    }

    public void Die()
    {
        // Add death logic here (e.g., play animation, drop loot, etc.)
        Destroy(gameObject);
    }
}
