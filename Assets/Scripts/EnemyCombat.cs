using Unity.VisualScripting;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int f_damage;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(f_damage);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(f_damage);
            }
    }
}
