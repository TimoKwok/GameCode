using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTrigger : MonoBehaviour
{
    bool playerDetection = false;
    public GameObject InteractPrompt;


    public GameObject screen;
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
                screen.SetActive(!screen.activeInHierarchy);
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
