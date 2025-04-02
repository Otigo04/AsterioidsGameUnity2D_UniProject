using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class SpaceshipAnimations : MonoBehaviour
{

    public Sprite idleSprite;
    public Sprite thrustSprite;
    public Sprite shootSprite;
    public Sprite thrustShootSprite;

    private SpriteRenderer spriteRenderer;
    private bool isShooting = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

        bool isThrusting = Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKeyDown(KeyCode.Space)) {
            isShooting = true;
            StopCoroutine("ResetShootingAfterDelay");
            StartCoroutine(ResetShootingAfterDelay(0.5f));
        }

        if (isThrusting && isShooting) {
            spriteRenderer.sprite = thrustShootSprite;
        }
        else if (isThrusting) {
            spriteRenderer.sprite = thrustSprite;
        }
        else if (isShooting) {
            spriteRenderer.sprite = shootSprite;
        }
        else {
            spriteRenderer.sprite = idleSprite;
        }

        IEnumerator ResetShootingAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);
            isShooting = false;
        }
    }
}
