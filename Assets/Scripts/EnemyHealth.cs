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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == DartLayer)
        {
            int damage = collision.gameObject.GetComponent<Dart_Script>().damage;
            TakeDamage(damage); // Adjust damage value as needed
            Debug.Log("Enemy hit! Current health: " + currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Add death logic here (e.g., play animation, drop loot, etc.)
        Destroy(gameObject);
    }
}
