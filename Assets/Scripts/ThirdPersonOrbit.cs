using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;

    public float xSpeed = 120f;
    public float ySpeed = 80f;

    public float yMinLimit = -40f;
    public float yMaxLimit = 80f;

    private float x;
    private float y;

    private Vector2 lookInput;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Apply input
        x += lookInput.x * xSpeed * Time.deltaTime;
        y -= lookInput.y * ySpeed * Time.deltaTime;

        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        transform.rotation = rotation;
        transform.position = position;
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}