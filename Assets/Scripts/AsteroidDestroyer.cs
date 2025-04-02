using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroyer : MonoBehaviour
{
    void OnBecameInvisible()
{
    Destroy(gameObject);
}

}
