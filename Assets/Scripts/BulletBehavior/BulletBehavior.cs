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
        HandleWrap();

        if (Time.time - spawnTime > lifetime)
        {
            Destroy(gameObject);
        }
    }

    void HandleWrap()
    {
        Vector3 pos = transform.position;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(pos);

        if (viewPos.x > 1) pos.x = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        else if (viewPos.x < 0) pos.x = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (viewPos.y > 1) pos.y = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        else if (viewPos.y < 0) pos.y = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        transform.position = pos;
    }
}
