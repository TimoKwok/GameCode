using System.Collections;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    bool playerDetection = false;
    public GameObject InteractPrompt;
    public AudioSource audioSource; // Assign your audio source component in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("Audio source not assigned dumb dumb!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleAudio();
            }
        }
    }

    void ToggleAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerBody")
        {
            playerDetection = true;
            InteractPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerDetection = false;
        InteractPrompt.SetActive(false);
    }
}