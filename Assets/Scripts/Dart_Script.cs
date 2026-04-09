using UnityEngine;

public class Dart_Script : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 5f;
    public float speed = 10f;

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
        if (CompareTag("Enemy"))
        {
            Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
        }
    }

}
