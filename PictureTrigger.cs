using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureTrigger : MonoBehaviour
{
    bool playerDetection = false;
    public GameObject BigPicture;
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
                BigPicture.SetActive(!BigPicture.activeInHierarchy);
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