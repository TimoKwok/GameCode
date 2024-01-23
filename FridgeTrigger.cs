using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeTrigger : MonoBehaviour
{
    bool playerDetection = false;
    public GameObject FridgePictures;
    public GameObject BackgroundBlur;
    public GameObject InteractPrompt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                InteractPrompt.SetActive(false);
                BackgroundBlur.SetActive(!BackgroundBlur.activeInHierarchy);
                FridgePictures.SetActive(!FridgePictures.activeInHierarchy);
            }
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
