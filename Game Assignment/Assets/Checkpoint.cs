using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    // The name of the scene to load after the player reaches the checkpoint
    public string nextLevelName;
    // This method gets called when the player enters the trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure the object is the player
        {
            // Load the next level (e.g., Level 2)
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
