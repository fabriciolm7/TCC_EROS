using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private AudioSource audioSource;

    public Transform firePoint;      // Ponto de tiro
    public GameObject bulletPrefab;  // Prefab do projétil
    public float fireRate = 1f;      // Tempo entre tiros
    public AudioClip attackSound;    // Som de ataque
    public AudioClip destroySound;   // Som de destruição

    [SerializeField]
    private Transform DetectaChao;
    [SerializeField]
    private LayerMask bricksLayer;   // Layer das paredes (Bricks)
    [SerializeField]
    private float distancia = 3f;
    [SerializeField]
    private float velocidade = 3f;

    private bool olhandoParaDireita = true;
    private bool isChasing = false;
    private float nextFireTime = 0f;
    private Transform player;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        anim.Play("boss_idle");
    }

    void Update()
    {
        if (!isChasing)
        {
            Patrulha();
        }
        else
        {
            Atacar();
        }
    }

    void Patrulha()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
        anim.Play("boss_run");

        // Detecta buraco
        RaycastHit2D groundInfo = Physics2D.Raycast(DetectaChao.position, Vector2.down, distancia);
        if (!groundInfo.collider)
        {
            InverterDirecao();
        }

        // Detecta parede
        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, Vector2.right * (olhandoParaDireita ? 1 : -1), 0.5f, bricksLayer);
        if (wallInfo.collider)
        {
            InverterDirecao();
        }
    }

    void Atacar()
    {
        rig.velocity = Vector2.zero;
        anim.Play("boss_attack");

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (player == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<HomingBullet>().SetTarget(player);

        PlaySound(attackSound);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
            anim.Play("boss_idle");
        }
    }

    private void InverterDirecao()
    {
        olhandoParaDireita = !olhandoParaDireita;
        transform.eulerAngles = new Vector3(0, olhandoParaDireita ? 0 : 180, 0);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
