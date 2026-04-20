using UnityEngine;

public class Obstacle_BouncePad : MonoBehaviour
{
    ObstacleParent obstacleParent;
    


    private void Start()
    {
        obstacleParent = GetComponentInParent<ObstacleParent>();
        obstacleParent.forceModifier = 10f;
        obstacleParent.rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = new Vector3(0, collision.gameObject.transform.position.y - transform.position.y, 0);

        obstacleParent.rb = collision.gameObject.GetComponent<Rigidbody>();

        float forceVariable = (obstacleParent.rb.linearVelocity.x + obstacleParent.rb.linearVelocity.y) * obstacleParent.forceModifier;

        if (forceVariable < 10f)
        {
            forceVariable = 10f;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            obstacleParent.rb.AddForce(direction * forceVariable, ForceMode.Impulse);
        }
    }
}
