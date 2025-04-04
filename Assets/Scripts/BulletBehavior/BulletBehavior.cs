using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float lifetime = 10f;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {

        if (Time.time - spawnTime > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
