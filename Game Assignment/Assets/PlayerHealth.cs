
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
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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
        // Check if the player has collided with an enemy
        if (collision.gameObject.CompareTag("Saw Blade Enemy"))
        {
            TakeDamage(3); // Adjust the damage amount as needed
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
        // You can add more logic here for restarting the level or showing a game over screen.

        Destroy(gameObject); // destroy the old player
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}


