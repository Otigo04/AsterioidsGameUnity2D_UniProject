using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroyer : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject, 3f); // Destroy nach 20 Sekunden
    }
}
