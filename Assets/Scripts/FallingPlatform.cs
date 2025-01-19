using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallingTime; // Tempo antes de a plataforma começar a cair
    public float playerTopOffset; // Ajuste para o local específico da parte superior do jogador
    public AudioClip fallingSound; // Som da queda

    private TargetJoint2D target;
    private AudioSource audioSource;

    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource não encontrado! Adicione um componente AudioSource ao objeto da plataforma.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtém a posição do jogador em relação à plataforma
            float playerY = collision.gameObject.transform.position.y;
            float platformY = transform.position.y + playerTopOffset;

            // Verifica se o jogador está acima da parte superior da plataforma
            if (playerY > platformY)
            {
                float disappearTime = fallingTime + 0.3f;

                // Inicia os métodos de queda e desaparecimento
                Invoke("Falling", fallingTime);
                Invoke("Disappear", disappearTime);

                // Toca o som de queda
                PlayFallingSound();
            }
        }
    }

    void Falling()
    {
        target.enabled = false;
    }

    void Disappear()
    {
        Destroy(gameObject);
    }

    void PlayFallingSound()
    {
        if (audioSource != null && fallingSound != null)
        {
            audioSource.clip = fallingSound;
            audioSource.loop = true; // Para tocar continuamente enquanto cai
            audioSource.Play();
        }
    }

    void StopFallingSound()
    {
        if (audioSource != null)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    void OnDestroy()
    {
        // Garante que o som seja parado quando a plataforma for destruída
        StopFallingSound();
    }
}
