using UnityEngine;

public class Dart_Script : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 5f;
    public float speed = 10f;

    public LayerMask enemyLayer;

    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * speed, ForceMode.Impulse);
        
        Invoke(nameof(OnDestroy), lifetime);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
        }
    }

}
