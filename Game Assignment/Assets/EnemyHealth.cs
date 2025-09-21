using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private Animator anim;
    private AudioSource audioSource;

    [Header("Audio")]
    public AudioClip deathSound;             // Assign in Inspector
    [Range(0f, 1f)] public float deathVolume = 0.8f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        Debug.Log(gameObject.name + " died!");

        // Play death sound
        if (deathSound != null && audioSource != null)
            audioSource.PlayOneShot(deathSound, deathVolume);

        // Optional: play death animation
        if (anim != null)
            anim.SetTrigger("die");

        // Disable colliders so the enemy can’t be hit again
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }

        // Destroy enemy after sound finishes (use clip length)
        float delay = (deathSound != null) ? deathSound.length : 1f;
        Destroy(gameObject, delay);
    }
}

