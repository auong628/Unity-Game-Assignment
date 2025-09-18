using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attackDamage = 1;
    public float attackRange = 2f;
    public Transform attackPoint; // Empty child object for attack position
    public LayerMask enemyLayers;
    public float attackRate = 2f; // attacks per second
    private float nextAttackTime = 0f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F)) // Press F to attack
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        anim.SetTrigger("attack");

        // Only detect enemies on the Enemy layer
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            LayerMask.GetMask("Enemy") // Force only Enemy layer
        );

        Debug.Log($"Attack triggered! Enemies hit: {hitEnemies.Length}");

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth eh = enemy.GetComponent<EnemyHealth>();

            if (eh != null)
            {
                Debug.Log($"✅ Damaging {enemy.gameObject.name}");
                eh.TakeDamage(attackDamage);
            }
            else
            {
                Debug.Log($"❌ {enemy.gameObject.name} has no EnemyHealth script");
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


public class AttackDebugger : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    void Update()
    {
        // Draw attack circle every frame while playing
        DebugDrawAttack();
    }

    void DebugDrawAttack()
    {
        if (attackPoint == null) return;

        // This will draw a yellow circle in Scene view while game runs
        Debug.DrawLine(attackPoint.position, attackPoint.position + Vector3.up * attackRange, Color.yellow);
        Debug.DrawLine(attackPoint.position, attackPoint.position + Vector3.down * attackRange, Color.yellow);
        Debug.DrawLine(attackPoint.position, attackPoint.position + Vector3.left * attackRange, Color.yellow);
        Debug.DrawLine(attackPoint.position, attackPoint.position + Vector3.right * attackRange, Color.yellow);

        // Actual overlap test
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if (hits.Length > 0)
        {
            foreach (Collider2D c in hits)
            {
                Debug.Log("DEBUG: Currently overlapping " + c.name);
            }
        }
    }
}
