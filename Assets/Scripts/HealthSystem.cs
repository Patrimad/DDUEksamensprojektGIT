using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action OnPlayerDied;

    public HealthUI healthUI;

    [Header("Max Stats")]
    public float maxHealth = 100f;

    [Header("Updated Stats")]
    public float currentHealth = 100f;
    public float healthRegenCooldown = 1f;
    public float healthRegenPerSecond = 10f;
    public float healthRegenTimer = 5f;

    public bool takingDamage = false;
    public bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead) return;

        HandleHealthRegen();
    }
    

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        takingDamage = true;
        CancelInvoke(nameof(ClearTakingDamage));
        Invoke(nameof(ClearTakingDamage), healthRegenCooldown);

        currentHealth = Mathf.Max(currentHealth - damage, 0f);

        if (currentHealth <= 0f)
        {
            isDead = true;
            OnPlayerDied?.Invoke();
        }
    }

    public void GiveHealth(int health)
    {
        currentHealth = Mathf.Min(currentHealth + health, maxHealth);
    }

    public void ChangeMaxHealth(int maxHealthMod)
    {
        maxHealth += maxHealthMod;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }
    

    private void HandleHealthRegen()
    {
        if (takingDamage)
            healthRegenTimer = 5f;

        if (healthRegenTimer > 0f)
        {
            healthRegenTimer -= Time.deltaTime;
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth + healthRegenPerSecond * Time.deltaTime, 0f, maxHealth);
    }

    private void ClearTakingDamage()
    {
        takingDamage = false;
    }
}