using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody2D;
    public float jumpForce;
    public bool isFirstJump;
    public bool isSecondJump;
    private Animator anim;
    private const int GroundLayer = 8;
    private AudioSource audioSource;
    public AudioClip gameOverSound; 

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource não encontrado! Por favor, adicione o componente ao jogador.");
        }
    }

    void Update()
    {
        if (DialogController.IsDialogActive)
        {
            anim.SetBool("walk", false); // Para a animação de andar
            return; 
        }
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("walk", false);
        }
        else
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, Input.GetAxis("Horizontal") > 0f ? 0f : 180f, 0f);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isFirstJump)
            {
                PerformJump();
                isFirstJump = true;
            }
            else if (!isSecondJump)
            {
                PerformJump();
                isSecondJump = true;
            }
        }
    }

    void PerformJump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
        rigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GroundLayer)
        {
            isFirstJump = false;
            isSecondJump = false;
        }

        if (collision.gameObject.tag == "Trap")
        {
            GameController.instance.ShowGameOver();

            // Debug para verificar se o som de Game Over está sendo tocado
            Debug.Log("Game Over! Tocando som...");

            // Toca o som de Game Over
            if (audioSource != null && gameOverSound != null)
            {
               audioSource.Play(); // Reproduz o som
            }
            else
            {
                Debug.LogError("AudioSource ou gameOverSound não estão configurados corretamente.");
            }

            Destroy(gameObject); // Destroi o jogador
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GroundLayer)
        {
            isFirstJump = true;
        }
    }
}
