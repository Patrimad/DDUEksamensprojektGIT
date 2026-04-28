using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image image;
    public HealthSystem HealthSystem;

    // Update is called once per frame
    void Update()
    { 
        float targetAlpha = 1f - (HealthSystem.currentHealth / HealthSystem.maxHealth);

        Color c = image.color;
        c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * 5f);
        image.color = c;
    }

    //public void CheckHealth()
    //{
    //    if (HealthSystem.currentHealth <= 0)
    //    {
    //        // Handle player death (e.g., show game over screen, restart level, etc.)          
    //        Debug.Log("Player has died!");
    //    }
    //}
}
