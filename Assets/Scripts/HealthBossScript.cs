using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBossScript : MonoBehaviour
{
    public int baseHealth = 100;
    public int strawberriesThreshold = 20; 
    public int healthReduction = 10; 
    private int bossHealth;

    void Start()
    {
        int totalStrawberries = GameController.totalStrawberriesCollected;

        // Calcula quantas vezes o jogador passou do limite de 20 morangos
        int reduction = (totalStrawberries / strawberriesThreshold) * healthReduction;

        bossHealth = Mathf.Max(baseHealth - reduction, 0);

        Debug.Log("Morangos coletados: " + totalStrawberries);
        Debug.Log("Vida do Boss: " + bossHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
