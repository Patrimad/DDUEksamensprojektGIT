using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    [Header("References")]
    public Rigidbody playerRigidbody;
    public ParticleSystem particles;

    [Header("Settings")]
    public float groundCheckDistance = 0.6f;
    public LayerMask groundMask;
    public float minSpeedToEmit = 0.5f;
    public float maxEmissionRate = 40f;
    public float maxSpeed = 8f;

    private ParticleSystem.EmissionModule emission;

    void Awake()
    {
        emission = particles.emission;
        emission.enabled = false;
    }

    void Update()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
        float speed = new Vector3(playerRigidbody.linearVelocity.x, 0, playerRigidbody.linearVelocity.z).magnitude;
        bool isRolling = speed > minSpeedToEmit;

        if (isGrounded && isRolling)
        {
            emission.enabled = true;
            float speedRatio = Mathf.Clamp01(speed / maxSpeed);
            emission.rateOverTime = speedRatio * maxEmissionRate;
        }
        else
        {
            emission.enabled = false;
        }
    }
}