using UnityEngine;

public class CollectibleLoot : MonoBehaviour
{
    public string itemName = "Mystic Gem";
    public Sprite itemIcon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Call UI Manager correctly
            UILootManager.Instance.ShowLootMessage(itemName, itemIcon);

            Destroy(gameObject);
        }
    }
}


