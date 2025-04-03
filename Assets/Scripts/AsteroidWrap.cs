using UnityEngine;

public class AsteroidWrap : MonoBehaviour
{
    private float wrapDelay = 3f;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }

    void FixedUpdate()
    {
        if (Time.time - spawnTime < wrapDelay) return; // noch zu früh, nicht wrappen

        Vector3 pos = transform.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(pos);

        if (viewportPos.x > 1) pos.x = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        else if (viewportPos.x < 0) pos.x = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (viewportPos.y > 1) pos.y = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        else if (viewportPos.y < 0) pos.y = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        transform.position = pos;
    }
}
