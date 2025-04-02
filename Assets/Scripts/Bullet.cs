using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject, 3f); // Destroy nach 3 Sekunden
    }
}
