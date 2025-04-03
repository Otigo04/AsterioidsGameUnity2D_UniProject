using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public bool isSmall = false; // Im Inspector setzen
    public GameObject smallAsteroidPrefab;
    public GameObject hitEffectPrefab;
    public int scoreOnDeath = 100;
    
    //ROTATION
    public float minRotationSpeed = -100f;
    public float maxRotationSpeed = 100f;


    private int currentHealth;

    void Start()
    {
        currentHealth = isSmall
            ? _Gamemanager.Instance.maxSmallAsteroidHealth
            : _Gamemanager.Instance.maxBigAsteroidHealth;

            float torque = Random.Range(minRotationSpeed, maxRotationSpeed);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddTorque(torque);

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
        for (int i = 0; i < 2; i++)
        {
            GameObject small = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = small.GetComponent<Rigidbody2D>();
            Vector2 dir = Random.insideUnitCircle.normalized;
            rb.velocity = dir * Random.Range(2f, 3.5f);
        }
    }
}
