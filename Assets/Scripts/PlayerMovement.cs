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

    [Header("Ground Detection")]
    public LayerMask groundLayer;

    [Header("Audio")]
    public AudioClip rollingClip;
    public float minRollSpeed = 0.1f;
    public float maxAcceleration = 2f;

    public AudioClip thumpClip;
    public float minImpactSpeed = 2f;
    public float maxImpactSpeed = 12f;

    private Rigidbody rb;
    [HideInInspector] public Vector2 moveInput;
    private bool isGrounded = false;

    private AudioSource rollingSource;
    private AudioSource thumpSource;

    private Vector3 lastFlatVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rollingSource = gameObject.AddComponent<AudioSource>();
        rollingSource.clip = rollingClip;
        rollingSource.loop = true;
        rollingSource.playOnAwake = false;
        rollingSource.volume = 0f;

        thumpSource = gameObject.AddComponent<AudioSource>();
        thumpSource.playOnAwake = false;

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

        float acceleration = (flatVelocity - lastFlatVelocity).magnitude / Time.fixedDeltaTime;
        lastFlatVelocity = flatVelocity;

        rb.AddForce(-flatVelocity * counterMovement, ForceMode.Impulse);

        float currentSpeed = Vector3.Dot(flatVelocity, moveDirection);
        if (currentSpeed < maxSpeed)
            rb.AddForce(moveDirection * force, ForceMode.Acceleration);

        HandleRollingAudio(flatVelocity.magnitude, acceleration);
    }

    void HandleRollingAudio(float speed, float acceleration)
    {
        if (speed > minRollSpeed && isGrounded)
        {
            if (!rollingSource.isPlaying)
                rollingSource.Play();

            rollingSource.volume = Mathf.Clamp01(acceleration / maxAcceleration);
        }
        else
        {
            if (rollingSource.isPlaying)
                rollingSource.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        float impact = collision.relativeVelocity.magnitude;
        if (impact < minImpactSpeed) return;

        float volume = Mathf.Clamp01((impact - minImpactSpeed) / (maxImpactSpeed - minImpactSpeed));
        thumpSource.PlayOneShot(thumpClip, volume);
    }

    void OnCollisionStay(Collision collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
            isGrounded = false;
    }
}