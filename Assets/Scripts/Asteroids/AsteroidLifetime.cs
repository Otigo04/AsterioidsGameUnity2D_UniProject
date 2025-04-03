using UnityEngine;

public class AsteroidLifetime : MonoBehaviour
{
    public float lifeTime = 20f;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - spawnTime > lifeTime)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

            if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
