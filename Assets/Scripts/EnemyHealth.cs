using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

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
        if (SpawnManager.Instance != null)
        {
            SpawnManager.Instance.ReportEnemyKilled();
        }
        Destroy(gameObject);
    }
}