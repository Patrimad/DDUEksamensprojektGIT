using Unity.VisualScripting;
using UnityEngine;

public class Obstacle_Pillar : MonoBehaviour
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
        Vector3 direction = new Vector3 (collision.gameObject.transform.position.x - transform.position.x, 0, collision.gameObject.transform.position.z - transform.position.z);
        
        obstacleParent.rb = collision.gameObject.GetComponent<Rigidbody>();

        float forceVariable = (obstacleParent.rb.linearVelocity.x + obstacleParent.rb.linearVelocity.y) *  obstacleParent.forceModifier;

        if (forceVariable < 30f)
        {
            forceVariable = 10f;
        }

        if (collision.gameObject.CompareTag("Player"))
        { 
            obstacleParent.rb.AddForce(direction * forceVariable, ForceMode.Impulse);
        }
    }


}
