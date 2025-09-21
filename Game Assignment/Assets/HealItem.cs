using UnityEngine;

public class HealItem : MonoBehaviour
{
    public int healAmount = 1;         // How much health this item restores
    public AudioClip pickupSound;      // Assign a pickup sound in the Inspector
    [Range(0f, 1f)] public float volume = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);

                // Play pickup sound before destroying
                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
                }

                Destroy(gameObject); // remove item after pickup
            }
        }
    }
}

public class ItemAnimation : MonoBehaviour
{
    public float floatSpeed = 2f;     // Up & down speed
    public float floatHeight = 0.25f; // Distance to float
    public float rotateSpeed = 90f;   // Degrees per second

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Floating up and down
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotate slowly
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}

