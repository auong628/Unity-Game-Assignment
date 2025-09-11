
using UnityEngine;

public class Molasses : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;

    private MolassesManager molassesManager;
    private void Start()
    {
        molassesManager = MolassesManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            molassesManager.ChangeMolasses(value);
            Destroy(gameObject);
        }
    }
}
