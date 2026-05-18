using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 25; // Adjust damage value as needed

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damageAmount); // Adjust damage value as needed
            }
        }
    }
}
