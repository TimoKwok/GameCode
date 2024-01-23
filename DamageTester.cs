using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public Health playerHealth;
    public PlayerMove playerMove;

    //void Start()
    //{
     //   if (playerHealth != null && playerMove != null)
       // {
         //   playerHealth.SetPlayerMove(playerMove);
        //}
        //else
        //{
         //   Debug.LogError("PlayerHealth or PlayerMove is not assigned in the GameManager!");
       // }
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerHealth.TakeDamage(1);
        }
    }
}
