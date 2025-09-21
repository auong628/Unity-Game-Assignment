using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOverManager : MonoBehaviour
{
    public static UIGameOverManager Instance;

    [Header("UI Elements")]
    public TMP_Text gameOverText;
    public Button restartButton;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Hide UI at start
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // Hook up button click
        restartButton.onClick.AddListener(RestartLevel);
    }

    public void ShowGameOver()
    {
        gameOverText.text = "You Died!";
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

