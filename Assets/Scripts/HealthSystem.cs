using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Playables;

public class HealthSystem : MonoBehaviour
{
    
    private Rigidbody rb;
    public HealthUI healthUI;
    CameraGunControll cameraGunControll;
    PlayerMovement playerMovement;

    public float counterMovement = 0.85f;


    [Header("MaxStats")]
    public float maxHealth = 100;
    
    
    [Header("Updated Stats")]
    public float currentHealth = 100;
    public float healthRegenCooldown = 1f;
    public float healthDrainPerSecond = 15f;
    public float healthRegenPerSecond = 10f;
    public float healthRegenTimer = 5f;
    
    public bool takingDamage = false;
    public bool isDead = false;

    private void Start()
    {
        cameraGunControll = GetComponent<CameraGunControll>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {

        if (isDead == true)
        {
            Die();
            return;
        }
        else
        {
            HandleHealthregen();
        }
    }

    
    
    public void Takedamage(int damage)
    {
        takingDamage = true;
        CancelInvoke(nameof(IsTakingDamage));
        Invoke(nameof(IsTakingDamage), healthRegenCooldown);

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        
        if (currentHealth <= 0)
        {
            isDead = true;
        }

    }

    public void Die()
    {
        cameraGunControll.enabled = false;
        playerMovement.enabled = false;

        Debug.Log("Player is Dead");
        
        rb.isKinematic = true;
    }

    public void GiveHealth(int health)
    {


        if (currentHealth + health >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }
            

        RegenerateHealth(Time.deltaTime);
    }
    public void ChangeMaxHealth(int maxHealthMod)
    {
        maxHealth += maxHealthMod;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        

    }
    
    public void RegenerateHealth(float deltaTime)
    {
       if (takingDamage)
       {
            healthRegenTimer = 5f;
       }

        if (healthRegenTimer > 0f)
        {
            healthRegenTimer -= deltaTime;
            return;
        }


        currentHealth += healthRegenPerSecond * deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    void HandleHealthregen()
    {    
           RegenerateHealth(Time.deltaTime);  
    }

    void IsTakingDamage()
    {
        takingDamage = false;
    }
}
