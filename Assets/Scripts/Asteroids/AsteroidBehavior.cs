using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public bool isSmall = false; // Optional: Inspector
    public GameObject smallAsteroidPrefab;

    public GameObject hitEffectPrefab;
    public int scoreOnDeath = 100;

    public float minRotationSpeed = -100f;
    public float maxRotationSpeed = 100f;

    private int currentHealth;

    void Start()
    {
        // Rotation zufällig beim Start
        float torque = Random.Range(minRotationSpeed, maxRotationSpeed);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddTorque(torque);
    }

    // ✅ Diese Initialisierung kommt vom Spawner!
    public void Initialize(int health, int score)
    {
        currentHealth = health;
        scoreOnDeath = score;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                int dmg = bullet.DoBulletDamage();
                TakeDamage(dmg);
                Destroy(other.gameObject);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            if (!isSmall)
            {
                SpawnSmalls();
            }

            _Gamemanager.Instance.AddScore(scoreOnDeath);
            Destroy(gameObject);
        }
    }

    void SpawnSmalls()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject small = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = small.GetComponent<Rigidbody2D>();
            Vector2 dir = Random.insideUnitCircle.normalized;
            rb.velocity = dir * Random.Range(2f, 3.5f);

            

            AsteroidBehavior behavior = small.GetComponent<AsteroidBehavior>();
            if (behavior != null)
            {
                behavior.Initialize(
                    _Gamemanager.Instance.maxSmallAsteroidHealth,
                    scoreOnDeath / 2 // Optional: Score halbieren für kleine
                );
            }
        }
    }
}
