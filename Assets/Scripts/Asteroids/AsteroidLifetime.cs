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

            if (viewPos.x < -0.2f || viewPos.x > 1.2f || viewPos.y < -0.2f || viewPos.y > 1.2f)
            {
                Destroy(gameObject);
            }
        }
    }
}
