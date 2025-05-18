using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSound : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip windSound; // Arraste o som do vento no Inspector

    void Start()
    {
        // Obtém o AudioSource no mesmo GameObject
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Nenhum AudioSource foi encontrado no GameObject. Adicione um componente AudioSource!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o jogador está colidindo com o ventilador
        if (collision.gameObject.tag == "Player1")
        {
            // Toca o som do vento, se houver um AudioClip configurado
            if (windSound != null && audioSource != null)
            {
                audioSource.clip = windSound;
                audioSource.loop = true; // O som continua enquanto o jogador está no ventilador
                audioSource.Play();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Quando o jogador sai do ventilador, o som para
        if (collision.gameObject.tag == "Player1")
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
