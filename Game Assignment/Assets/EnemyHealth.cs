using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health now = {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Condition reached: health <= 0"); // 👈 NEW
            Die();
        }
    }


    void Die()
    {
        Debug.Log(gameObject.name + " died! Destroying now.");
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }
        Destroy(gameObject);
    }


}
