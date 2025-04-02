using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 10f;

    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 1f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        Vector2 spawnPos = GetRandomEdgePosition();
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        Vector2 direction = (Vector2.zero - spawnPos).normalized;
        rb.velocity = direction * Random.Range(1f, 3f);
    }

    Vector2 GetRandomEdgePosition()
    {
        Camera cam = Camera.main;
        float screenX = Random.value < 0.5f ? 0f : 1f;
        float screenY = Random.value;
        if (Random.value < 0.5f)
        {
            // Oben oder Unten
            screenY = Random.value < 0.5f ? 0f : 1f;
            screenX = Random.value;
        }

        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(screenX, screenY, 0));
        return new Vector2(worldPos.x, worldPos.y);
    }
}
