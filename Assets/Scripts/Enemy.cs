using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public Transform headPoint;
    private bool colliding;

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private float jumpForce;

    public Transform DetectaChao;
    public float distancia = 3;
    public bool olhandoParaDireita;
    public float velocidade = 4;
    public bool playerDestroyed = true;
    public AudioClip destroySound;
    private AudioSource audioSource;
    private bool isDead = false;

    void Start()
    {
        olhandoParaDireita = true;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerDestroyed = false;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);

        if (audioSource == null)
        {
            Debug.LogError("AudioSource nao encontrado! Adicione um componente AudioSource ao objeto da plataforma.");
        }
    }

    void Update()
    {
        Patrulha();
        DetectaParede();
    }

    public void Patrulha()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(DetectaChao.position, Vector2.down, distancia);
        if (!groundInfo.collider)
        {
            InverterDirecao();
        }
    }

    public void DetectaParede()
    {
        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, Vector2.right * (olhandoParaDireita ? 1 : -1), 0.1f, layer);
        if (wallInfo.collider)
        {
            InverterDirecao();
        }
    }

    private void InverterDirecao()
    {
        transform.eulerAngles = new Vector3(0, olhandoParaDireita ? -180 : 0, 0);
        olhandoParaDireita = !olhandoParaDireita;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player1")
        {
            float height = col.contacts[0].point.y - headPoint.position.y;

            if (height > 0)
            {
                rig.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetTrigger("die");

                // Impede o inimigo de causar dano novamente
                GetComponent<Collider2D>().enabled = false;
                rig.simulated = false;

                Destroy(gameObject, 0.1f);
            }
            else
            {
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(col.gameObject);
            }
        }
    }

    void PlayDestroySound()
    {
        if (audioSource != null && destroySound != null)
        {
            audioSource.clip = destroySound;
            audioSource.loop = true; // Para tocar continuamente enquanto cai
            audioSource.Play();
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        StopFallingSound();
    }

    void StopFallingSound()
    {
        if (audioSource != null)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
}