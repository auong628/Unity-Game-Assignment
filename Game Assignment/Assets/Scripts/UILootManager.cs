using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UILootManager : MonoBehaviour
{
    public static UILootManager Instance; // Singleton reference

    public TMP_Text lootText;   // Assign a TextMeshProUGUI object
    public Image lootIcon;      // Assign a UI Image object
    public float displayTime = 2f;  // Time before fade starts
    public float fadeDuration = 1f; // Fade-out duration

    private Coroutine hideCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        lootText.gameObject.SetActive(false);
        lootIcon.gameObject.SetActive(false);
    }

    // Show loot collected
    public void ShowLootMessage(string lootName, Sprite lootSprite)
    {
        lootText.text = "Collected: " + lootName;
        lootIcon.sprite = lootSprite;

        lootText.gameObject.SetActive(true);
        lootIcon.gameObject.SetActive(true);

        // Reset alpha to fully visible
        SetAlpha(1f);

        Debug.Log("Loot UI updated with: " + lootName);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(FadeOutAfterDelay());
    }

    private IEnumerator FadeOutAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        // Hide completely after fade
        lootText.gameObject.SetActive(false);
        lootIcon.gameObject.SetActive(false);
    }

    // Helper to set alpha of both UI elements
    private void SetAlpha(float alpha)
    {
        Color textColor = lootText.color;
        textColor.a = alpha;
        lootText.color = textColor;

        Color iconColor = lootIcon.color;
        iconColor.a = alpha;
        lootIcon.color = iconColor;
    }
}


