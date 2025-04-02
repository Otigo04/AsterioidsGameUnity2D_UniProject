using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroiden-Einstellungen")]
    public GameObject asteroidPrefab;

    [Tooltip("Zeit zwischen Spawns in Sekunden")]
    public float spawnInterval = 2f;

    [Tooltip("Zufällige Geschwindigkeit zwischen diesen Werten")]
    public Vector2 speedRange = new Vector2(1f, 10f);

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefab == null)
        {
            Debug.LogWarning("AsteroidPrefab ist nicht zugewiesen");
            return;
        }

        Vector2 spawnPos = GetRandomEdgePosition();
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        Vector2 direction = (Vector2.zero - spawnPos).normalized;

        float speed = Random.Range(speedRange.x, speedRange.y);
        rb.velocity = direction * speed;
    }

    Vector2 GetRandomEdgePosition()
    {
        Camera cam = Camera.main;
        float screenX = Random.value;
        float screenY = Random.value;
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: screenY = 1.1f; break; // oben
            case 1: screenY = -0.1f; break; // unten
            case 2: screenX = -0.1f; break; // links
            case 3: screenX = 1.1f; break; // rechts
        }

        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(screenX, screenY, 0));
        return new Vector2(worldPos.x, worldPos.y);
    }
}
