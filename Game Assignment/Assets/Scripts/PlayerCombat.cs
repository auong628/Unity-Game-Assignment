using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attackDamage = 1;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    private Animator anim;
    private AudioSource audioSource;

    [Header("Attack Audio")]
    public AudioClip attackSound;   // Sword swing
    public AudioClip hitSound;      // Enemy hit
    [Range(0f, 1f)] public float attackVolume = 0.7f; // Volume slider for swing
    [Range(0f, 1f)] public float hitVolume = 0.8f;    // Volume slider for hit

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Play attack animation
        anim.SetTrigger("attack");

        // Play attack sound at chosen volume
        if (attackSound != null)
            audioSource.PlayOneShot(attackSound, attackVolume);

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit enemy: " + enemy.name);

            // Play hit sound if enemy is struck
            if (hitSound != null)
                audioSource.PlayOneShot(hitSound, hitVolume);

            // Check for regular enemies
            EnemyHealth enemyHealth = enemy.GetComponentInParent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                continue; // skip further checks, no need to damage twice
            }

            // Check for bosses
            BossHealth bossHealth = enemy.GetComponentInParent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

