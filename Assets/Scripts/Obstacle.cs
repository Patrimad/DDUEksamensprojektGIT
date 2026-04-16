using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float forceModifier = 5f;
    Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = new Vector3 (collision.gameObject.transform.position.x - transform.position.x, collision.gameObject.transform.position.y - transform.position.y, 0);
        
        rb = collision.gameObject.GetComponent<Rigidbody>();

        float forceVariable = (rb.linearVelocity.x + rb.linearVelocity.y) * forceModifier;

        if (forceVariable < 10f)
        {
            forceVariable = 10f;
        }

        if (collision.gameObject.CompareTag("Player"))
        { 
            rb.AddForce(direction * forceVariable, ForceMode.Impulse);
        }
    }

}
