using UnityEngine;

public class Dart_Script : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 5f;
    public LayerMask enemyLayer;

    void Awake()
    {
        Invoke(nameof(SelfDestruct), lifetime);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}