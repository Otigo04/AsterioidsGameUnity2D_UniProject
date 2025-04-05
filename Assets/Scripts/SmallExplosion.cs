using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallExplosion : MonoBehaviour
{
    public float deletePrefabDelay = 0f;
    void Start()
    {
        Destroy(gameObject, deletePrefabDelay);
    } 
}
