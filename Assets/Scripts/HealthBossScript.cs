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
    public Vector3 offset = new Vector3(0, 2f, 0); 

    private Camera mainCamera;
    private RectTransform sliderRectTransform;

    void Awake()
    {
        mainCamera = Camera.main;
        Debug.Log("Main Camera: " + mainCamera?.name);
    }

    void Start()
    {
        Debug.Log("Boss current health: " + GameController.bossCurrentHealth);
        int totalStrawberries = GameController.totalStrawberriesCollected;
        float reduction = totalStrawberries * 0.5f;

        maxHealth = baseHealth;

        if (GameController.bossCurrentHealth > 0)
        {
            bossHealth = GameController.bossCurrentHealth;
        }
        else 
        {
            bossHealth = Mathf.Max(baseHealth - reduction, 10f);
            GameController.bossCurrentHealth = bossHealth;

            var dialog = FindObjectOfType<DialogController>();
            if (dialog != null)
            {
                dialog.MostrarDialogo(totalStrawberries, reduction);
            }
        }

        UpdateHealthBar();

        if (healthBarSlider != null)
        {
            sliderRectTransform = healthBarSlider.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogWarning("Slider de vida nao atribuido!");
        }
    }


    void Update()
    {
        if (mainCamera != null && sliderRectTransform != null)
        {
            Vector3 worldPosition = transform.position + offset;
            Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPosition);

            if (screenPos.z > 0)
            {
                Vector2 localPoint;
                RectTransform canvasRect = sliderRectTransform.root.GetComponent<RectTransform>();

                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, mainCamera, out localPoint))
                {
                    sliderRectTransform.localPosition = localPoint;
                }
            }
            else
            {
                Debug.LogWarning("Boss est� atr�s da c�mera! A barra de vida n�o ser� renderizada.");
            }
        }
    }


    public void TakeDamage(float amount)
    {
        bossHealth -= amount;
        bossHealth = Mathf.Max(bossHealth, 0f);
        GameController.bossCurrentHealth = bossHealth; 
        UpdateHealthBar();

        Debug.Log("Boss recebeu dano! Vida restante: " + bossHealth);

        if (bossHealth <= 0f)
        {
            Debug.Log("Boss derrotado!");
            var dialog = FindObjectOfType<DialogController>();
            if (dialog != null)
            {
                dialog.MostrarDialogoFinal();
            }
        }

        GameController.bossCurrentHealth = bossHealth;
        PlayerPrefs.SetFloat("BossHealth", bossHealth);
        PlayerPrefs.Save();
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
