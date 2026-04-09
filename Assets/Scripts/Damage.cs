using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.Takedamage(25); // Adjust damage value as needed
            }
        }
    }
}
