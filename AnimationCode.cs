using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCode : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWasd = (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"));
        // Check if the "W" key is pressed
        if (isWasd)
        {
            // Set the "isRunning" parameter to true
            animator.SetBool("isRunning", true);
        }
        else
        {
            // If the "W" key is not pressed, set the "isRunning" parameter to false
            animator.SetBool("isRunning", false);
        }
    }
}
