using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AsteroidType
{
    public GameObject prefab;
    public float spawnChance;
    public int scoreValue;
}

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroiden-Einstellungen")]
    public float spawnInterval = 2f;
    public Vector2 speedRange = new Vector2(1f, 10f);
    public List<AsteroidType> asteroidTypes = new List<AsteroidType>();

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        Vector2 spawnPos = GetRandomEdgePosition();

        GameObject prefab = GetRandomAsteroidPrefab(out int scoreValue);

        if (prefab == null)
        {
            Debug.LogWarning("Kein Asteroiden-Prefab ausgewählt!");
            return;
        }

        GameObject asteroid = Instantiate(prefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        Vector2 direction = Random.insideUnitCircle.normalized;
        float speed = Random.Range(speedRange.x, speedRange.y);
        rb.velocity = direction * speed;

        // Score im Asteroid setzen
        AsteroidBehavior behavior = asteroid.GetComponent<AsteroidBehavior>();
        if (behavior != null)
        {
            behavior.scoreOnDeath = scoreValue;
        }
    }

    GameObject GetRandomAsteroidPrefab(out int score)
    {
        float totalChance = 0f;
        foreach (var type in asteroidTypes)
        {
            totalChance += type.spawnChance;
        }

        float randomValue = Random.Range(0, totalChance);
        float cumulative = 0f;

        foreach (var type in asteroidTypes)
        {
            cumulative += type.spawnChance;
            if (randomValue <= cumulative)
            {
                score = type.scoreValue;
                return type.prefab;
            }
        }

        score = 0;
        return null;
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
