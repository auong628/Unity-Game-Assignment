using UnityEngine;
using TMPro;
using System.Collections;

public class UIBossManager : MonoBehaviour
{
    public static UIBossManager Instance;

    public TMP_Text bossDefeatedText; // Assign in Inspector
    public float showTime = 2f;       // How long to stay before fade
    public float fadeDuration = 2f;   // Fade-out time
    public float scaleUp = 1.5f;      // Dramatic scale effect

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (bossDefeatedText != null)
            bossDefeatedText.gameObject.SetActive(false);
    }

    public void ShowBossDefeated()
    {
        if (bossDefeatedText == null)
        {
            Debug.LogError("BossDefeatedText is NOT assigned in UIBossManager!");
            return;
        }

        StopAllCoroutines(); // Prevent overlapping animations
        StartCoroutine(DramaticTextEffect());
    }

    private IEnumerator DramaticTextEffect()
    {
        bossDefeatedText.text = "Boss Defeated!";
        bossDefeatedText.gameObject.SetActive(true);

        // Reset
        bossDefeatedText.color = Color.white;
        bossDefeatedText.transform.localScale = Vector3.one;

        // Scale up (dramatic punch)
        float t = 0f;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(1f, scaleUp, t / 0.3f);
            bossDefeatedText.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        // Hold text for a moment
        yield return new WaitForSeconds(showTime);

        // Fade out (keep the big scale)
        float elapsed = 0f;
        Color startColor = bossDefeatedText.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            bossDefeatedText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        bossDefeatedText.gameObject.SetActive(false);
        bossDefeatedText.transform.localScale = Vector3.one; // Reset
        bossDefeatedText.color = startColor; // Reset for next time
    }
}


