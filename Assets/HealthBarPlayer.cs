using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    public Slider healthBarFill; 
    public GameObject gameOverPanel;
    private Animator anim;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        anim.SetTrigger("Hit");

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
            GameController.instance.ShowGameOver();
        }
    }
        
    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.maxValue = maxHealth;
            healthBarFill.value = currentHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    void Die()
    {
        Debug.Log("Jogador morreu");
        GameController.instance.ShowGameOver();
        gameObject.SetActive(false);
    }
}
