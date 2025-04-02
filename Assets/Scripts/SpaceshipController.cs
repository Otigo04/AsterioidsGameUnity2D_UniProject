using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class SpaceshipController : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public float thrustForce = 5f;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;
    public Transform leftGun;
    public Transform rightGun;
    public float bulletSpeed = 10f;
    public TextMeshProUGUI hptext;

    // HEALTH
    public int maxHP = 3;
    private int currentHP;

    //CANSHOOT
    public float shootCooldown = 0.2f;
    public bool canShoot = true;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        currentHP = maxHP;
    }

    // Update is called once per framee
    void Update()
    {
        float rotate = -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotate);
        
        if (Input.GetKey(KeyCode.UpArrow)) {
            UnityEngine.Vector2 force = transform.up * thrustForce;
            rb.AddForce(force);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canShoot) {
        
            FireBullet(leftGun);
            FireBullet(rightGun);
            StartCoroutine(ShootCooldown());

        }

        hptext.text = "Raumschiff HP: " + currentHP.ToString();

    }

    void FixedUpdate()
    {
        WrapAroundScreen();   
    }

    void WrapAroundScreen() {
        UnityEngine.Vector2 newPosition = transform.position;
        UnityEngine.Vector2 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        if (screenPos.x > 1) newPosition.x = -newPosition.x;
        else if (screenPos.x < 0) newPosition.x = -newPosition.x;

        if (screenPos.y > 1) newPosition.y = -newPosition.y;
        else if (screenPos.y < 0) newPosition.y = -newPosition.y;

        transform.position = newPosition;
    }

    void FireBullet (Transform firePoint) {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }

    IEnumerator ShootCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}
