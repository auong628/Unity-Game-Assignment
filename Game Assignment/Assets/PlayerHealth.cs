using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Image[] heartContainers; // Array for heart UI images
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Audio")]
    public AudioClip hurtSound; // Sound when taking damage
    public AudioClip deathSound; // Sound when dying
    [Range(0f, 1f)] public float volume = 1f;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (hurtSound != null && currentHealth > 0)
        {
            AudioSource.PlayClipAtPoint(hurtSound, transform.position, volume);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        UpdateHearts();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < currentHealth)
            {
                heartContainers[i].sprite = fullHeart;
            }
            else
            {
                heartContainers[i].sprite = emptyHeart;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Saw Blade Enemy"))
        {
            TakeDamage(3);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Heal(1);
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");

        // Show Game Over UI instead of instantly restarting
        UIGameOverManager.Instance.ShowGameOver();

        // Disable movement + combat so player can't do anything
        if (GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = false;

        if (GetComponent<PlayerCombat>() != null)
            GetComponent<PlayerCombat>().enabled = false;

        // Optional: disable collider so enemies stop interacting with the player
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // ❌ Don't destroy the player here
        // Destroy(gameObject); ← remove this line
    }

}


