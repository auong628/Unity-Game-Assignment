
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
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
    }
}


