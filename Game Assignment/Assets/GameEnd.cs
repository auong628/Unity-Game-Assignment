using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public GameObject endingUI; // UI panel to show when the game ends
    public string endingMessage = "Congratulations! You Win!";

    private void Start()
    {
        if (endingUI != null)
            endingUI.SetActive(false); // Hide at start
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if (endingUI != null)
        {
            endingUI.SetActive(true);
            UnityEngine.UI.Text text = endingUI.GetComponentInChildren<UnityEngine.UI.Text>();
            if (text != null)
                text.text = endingMessage;
        }

        // Optional: stop time so game freezes
        Time.timeScale = 0f;
    }

    // Can be called from a button in your UI
    public void QuitGame()
    {
        // If you’re in the editor it won’t quit, but in a build it will
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}
