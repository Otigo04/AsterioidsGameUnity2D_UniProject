using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AsteroidType
{
    public GameObject prefab;
    [Range(0, 100)] public float spawnChancePercent;
    public int scoreValue;
    public int asteroidHealth;
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

        AsteroidType selectedType = GetRandomAsteroidType();
        if (selectedType == null)
        {
            Debug.LogWarning("Kein Asteroiden-Typ gefunden! Bitte überprüfe die Wahrscheinlichkeiten.");
            return;
        }

        GameObject asteroid = Instantiate(selectedType.prefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        Vector2 direction = Random.insideUnitCircle.normalized;
        float speed = Random.Range(speedRange.x, speedRange.y);
        rb.velocity = direction * speed;

        AsteroidBehavior behavior = asteroid.GetComponent<AsteroidBehavior>();
        if (behavior != null)
        {
            behavior.Initialize(selectedType.asteroidHealth, selectedType.scoreValue);
        }
    }

    AsteroidType GetRandomAsteroidType()
    {
        float total = 0f;

        // Sicherheitsprüfung: Gesamtwert berechnen
        foreach (var type in asteroidTypes)
        {
            total += type.spawnChancePercent;
        }

        if (total <= 0f)
        {
            Debug.LogWarning("Spawn Wahrscheinlichkeiten ergeben 0%! Bitte anpassen.");
            return null;
        }

        float randomPoint = Random.Range(0f, total);
        float cumulative = 0f;

        foreach (var type in asteroidTypes)
        {
            cumulative += type.spawnChancePercent;
            if (randomPoint <= cumulative)
            {
                return type;
            }
        }

        // Sicherheit, falls floating point Ungenauigkeit
        return asteroidTypes[asteroidTypes.Count - 1];
    }

    Vector2 GetRandomEdgePosition()
    {
        Camera cam = Camera.main;
        float screenX = Random.value;
        float screenY = Random.value;
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: screenY = 1.2f; break; // oben
            case 1: screenY = -0.2f; break; // unten
            case 2: screenX = -0.2f; break; // links
            case 3: screenX = 1.2f; break; // rechts
        }

        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(screenX, screenY, 0));
        return new Vector2(worldPos.x, worldPos.y);
    }
}
