using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Playables;

public class HealthSystem : MonoBehaviour
{
    
    private Rigidbody rb;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject deathDisplay;

    public HealthUI healthUI;
    public PlayerInput playerInput;
    public CinemachineCamera aimCamera;
    public CinemachineCamera freeLookCam;

    CinemachineBrain cinemachineBrain;
    Camera mainCam;
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
        mainCam = Camera.main;
        cinemachineBrain = mainCam.GetComponent<CinemachineBrain>();
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
        playerInput.actions["Look"].Disable();
        playerInput.actions["Attack"].Disable();
        playerInput.actions["Aim"].Disable();

        cinemachineBrain.enabled = false;
        cameraGunControll.enabled = false;
        freeLookCam.enabled = false;
        aimCamera.enabled = false;
        //playerMovement.enabled = false;

        crosshair.SetActive(false);
        deathDisplay.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Player is Dead");
        
        //rb.isKinematic = true;

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
