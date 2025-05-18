using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    public GameObject collected; // Objeto que ser� ativado quando o morango for coletado
    public int score; // Pontua��o obtida ao coletar o morango

    private AudioSource audioSource;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();

        // Inicializa o AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource n�o encontrado no objeto do morango!");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player1"))
        {
            // Desativa o sprite e o colisor do morango
            sr.enabled = false;
            circle.enabled = false;

            // Ativa o efeito coletado, se existir
            if (collected != null)
            {
                collected.SetActive(true);
            }

            // Toca o �udio se o AudioSource estiver configurado
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            // Atualiza a pontua��o
            GameController.instance.totalScore += score;
            GameController.instance.UpdateScore();
            GameController.instance.AddStrawberry();

            // Destroi o objeto ap�s o t�rmino do �udio ou ap�s um pequeno atraso
            Destroy(gameObject, audioSource != null ? audioSource.clip.length : 0.25f);
        }
    }
}
