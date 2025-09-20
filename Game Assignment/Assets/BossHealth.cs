using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public GameObject lootPrefab; // Assign your loot prefab here

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health now = {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");

        // Drop loot if needed
        if (lootPrefab != null)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }

        // Show Boss Defeated popup
        UIBossManager.Instance.ShowBossDefeated();

        Destroy(gameObject);
    }


}
