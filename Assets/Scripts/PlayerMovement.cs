using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float force = 10f;
    public float maxSpeed = 8f;
    [Range(0f, 1f)]
    public float counterMovement = 0.85f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * moveInput.y + camRight * moveInput.x;

        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(-flatVelocity * counterMovement, ForceMode.Impulse);

        float currentSpeed = Vector3.Dot(flatVelocity, moveDirection);
        if (currentSpeed < maxSpeed)
        {
            rb.AddForce(moveDirection * force, ForceMode.Acceleration);
        }
    }
}