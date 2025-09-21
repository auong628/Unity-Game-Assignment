using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip clickSound;    // The sound to play

    // Call this function from the Button's OnClick event
    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}

