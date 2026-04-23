using UnityEngine;

public class Obstacle_BouncePad : MonoBehaviour
{
    ObstacleParent obstacleParent;
    public float forceModifier = 0;


    private void Start()
    {
        obstacleParent = GetComponentInParent<ObstacleParent>();
        obstacleParent.rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = new Vector3(0, collision.gameObject.transform.position.y - transform.position.y, 0);

        obstacleParent.rb = collision.gameObject.GetComponent<Rigidbody>();

        float forceVariable = Mathf.Abs(obstacleParent.rb.linearVelocity.y) * forceModifier;

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
