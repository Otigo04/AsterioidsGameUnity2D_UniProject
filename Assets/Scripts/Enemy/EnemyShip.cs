using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Ziel")]
    public Transform player;

    [Header("Bewegung")]
    public float moveSpeed = 2f;

    [Header("Schießen")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f; // Schüsse pro Sekunde
    private float fireCooldown;

    [Header("Explosion")]
    public GameObject explosionPrefab;
    public GameObject deathPrefab;

    public int health = 3;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        fireCooldown = 1f / fireRate;
    }

    void Update()
    {
        if (player == null) return;

        // Bewegung
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        // Sanfte Rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180 * Time.deltaTime);

        // Schießen
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            FireBullet();
            fireCooldown = 1f / fireRate;
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.up * 10f;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
