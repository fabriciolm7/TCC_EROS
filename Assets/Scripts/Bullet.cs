using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 6f;
    public float rotateSpeed = 300f;
    public float lifeTime = 5f;
    private Transform target;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            HealthBarPlayer health = other.GetComponent<HealthBarPlayer>();

            if (health == null)
            {
                health = other.GetComponentInChildren<HealthBarPlayer>();
            }

            if (health != null)
            {
                health.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}
