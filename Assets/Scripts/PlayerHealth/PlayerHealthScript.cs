using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("Health Einstellungen")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("UI")]
    public TextMeshProUGUI hptext;
    public GameObject gameOverPanel;

    [Header("Steuerung")]
    public GameObject playerControls; // Hier dein Steuerungs-Script oder -Objekt

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (hptext != null)
            hptext.text = "Raumschiff HP: " + currentHealth.ToString();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // UI Health Anzeige updaten
        if (hptext != null)
            hptext.text = "Raumschiff HP: " + currentHealth.ToString();

        // Restart Option wenn tot
        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("U DIED!");

        // Steuerung deaktivieren
        if (playerControls != null)
            playerControls.SetActive(false);

        // Game Over Panel aktivieren
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Optional: Cursor freigeben (falls benötigt)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return; // Keine Kollisionen mehr wenn tot

        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Asteroid"))
        {
            TakeDamage(1);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
