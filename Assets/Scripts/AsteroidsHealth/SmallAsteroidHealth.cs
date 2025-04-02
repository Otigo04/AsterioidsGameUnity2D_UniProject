using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroidHealth : MonoBehaviour
{

    public int currentSmallAsteroidHealth;
    public int maxSmallAsteroidHealth = 2;
    void Start()
    {
        currentSmallAsteroidHealth = maxSmallAsteroidHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSmallAsteroidHealth <= 0) {
                Destroy(gameObject);
            }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            Debug.Log("HIT!");
            currentSmallAsteroidHealth--;

        }
    }
}
