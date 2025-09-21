using UnityEngine;

public class CollectibleLoot : MonoBehaviour
{
    public string itemName = "Mystic Gem"; // Name to display in UI
    public Sprite itemIcon;                // Icon for UI
    public AudioClip pickupSound;          // Assign in Inspector
    [Range(0f, 1f)] public float volume = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Call UI Manager to show message
            UILootManager.Instance.ShowLootMessage(itemName, itemIcon);

            // Play pickup sound
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
            }

            Destroy(gameObject); // remove item after pickup
        }
    }
}



