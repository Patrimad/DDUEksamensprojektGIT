using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2.0f;

    private int _currentIndex = 0;

    void Start()
    {
        if (points.Length > 0)
        {
            transform.position = points[0].transform.position;
        }
    }

    void Update()
    {
        if (points.Length < 2)
        {
            return;
        }

        Vector3 target = points[_currentIndex].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            _currentIndex = (_currentIndex + 1) % points.Length;
        }
    }
}
