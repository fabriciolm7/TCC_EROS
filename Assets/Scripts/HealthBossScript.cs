using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBossScript : MonoBehaviour
{
    public int baseHealth = 100;
    public int strawberriesThreshold = 20;
    public int healthReduction = 1;
    private int bossHealth;
    private int maxHealth;
    public Slider healthBarSlider;

    void Start()
    {
        int totalStrawberries = GameController.totalStrawberriesCollected;
        int reduction = (totalStrawberries / strawberriesThreshold) * healthReduction;

        bossHealth = Mathf.Max(baseHealth - reduction, 0);
        maxHealth = bossHealth;

        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        bossHealth -= amount;
        bossHealth = Mathf.Max(bossHealth, 0);
        UpdateHealthBar();

        Debug.Log("Boss recebeu dano! Vida restante: " + bossHealth);

        if (bossHealth <= 0)
        {
            Debug.Log("Boss derrotado!");
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = bossHealth;
        }
    }
}
