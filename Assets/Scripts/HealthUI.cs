using UnityEngine;
using UnityEngine.UI;

public class HealthUI1 : MonoBehaviour
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
}
