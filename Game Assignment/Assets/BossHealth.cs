using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Loot Drop")]
    public GameObject lootPrefab; // Assign loot prefab here

    [Header("Audio")]
    public AudioClip deathSound;             // Assign boss death sound
    [Range(0f, 1f)] public float deathVolume = 1f;

    private AudioSource audioSource;
    private SpriteRenderer[] spriteRenderers; // supports multiple sprites

    private void Awake()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(); // catch all child sprites
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health now = {currentHealth}");

        if (currentHealth <= 0)
        {
            StartCoroutine(FadeAndDie());
        }
    }

    private IEnumerator FadeAndDie()
    {
        Debug.Log("Boss defeated!");

        // Play death sound
        if (deathSound != null && audioSource != null)
            audioSource.PlayOneShot(deathSound, deathVolume);

        // Drop loot if assigned
        if (lootPrefab != null)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }

        // Show Boss Defeated popup
        UIBossManager.Instance.ShowBossDefeated();

        // Fade out all sprite renderers
        float duration = 6f;
        float elapsed = 0f;

        // Cache starting colors
        Color[] startColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            startColors[i] = spriteRenderers[i].color;
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);

            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = new Color(
                    startColors[i].r,
                    startColors[i].g,
                    startColors[i].b,
                    alpha
                );
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}

