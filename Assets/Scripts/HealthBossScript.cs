using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBossScript : MonoBehaviour
{
    public float baseHealth = 100f;
    private float bossHealth;
    private float maxHealth;
    public Slider healthBarSlider;

    void Start()
    {
        int totalStrawberries = GameController.totalStrawberriesCollected;
        float reduction = totalStrawberries * 0.5f;

        bossHealth = Mathf.Max(baseHealth - reduction, 10f);
        maxHealth = baseHealth; 

        UpdateHealthBar();

        var dialog = FindObjectOfType<DialogController>();
        if (dialog != null)
        {
            dialog.MostrarDialogo(totalStrawberries, reduction);
        }
    }

    public void TakeDamage(float amount)
    {
        bossHealth -= amount;
        bossHealth = Mathf.Max(bossHealth, 0f);
        UpdateHealthBar();

        Debug.Log("Boss recebeu dano! Vida restante: " + bossHealth);

        if (bossHealth <= 0f)
        {
            Debug.Log("Boss derrotado!");
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = baseHealth; 
            healthBarSlider.value = bossHealth;    
        }
    }
}
