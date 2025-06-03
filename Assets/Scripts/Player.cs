using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody2D;
    public float jumpForce;
    private Animator anim;

    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    private bool canDoubleJump;

    private AudioSource audioSource;
    public AudioClip gameOverSound;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource n�o encontrado! Por favor, adicione o componente ao jogador.");
        }
    }

    void Update()
    {
        if (DialogController.IsDialogActive)
        {
            anim.SetBool("walk", false); 
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
        bool grounded = IsGrounded();

        if (grounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                PerformJump();
            }
            else if (canDoubleJump)
            {
                PerformJump();
                canDoubleJump = false;
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

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            GameController.instance.ShowGameOver();

            Debug.Log("Game Over! Tocando som...");

            if (audioSource != null && gameOverSound != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogError("AudioSource ou gameOverSound n�o est�o configurados corretamente.");
            }

            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
