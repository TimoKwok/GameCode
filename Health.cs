using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfhearts;
    public float regenerationTime = 5f;
    
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool isRegenerating = false;


    public PlayerMove playerMove;


    public void TakeDamage(int amount)
    {
        if (!isRegenerating)
        {
            health -= amount;
            
            if (health == 0)
            {
                StartCoroutine(RegenerateHearts());
            }
        }
        
    }

    IEnumerator RegenerateHearts()
    {
        isRegenerating = true;
        playerMove.moveSpeed = 0;
        yield return new WaitForSeconds(regenerationTime);

        health = numOfhearts;
        isRegenerating = false;
        playerMove.moveSpeed = 6;
    }
    
    void Update()
    {

        if (health > numOfhearts)
        {
            health = numOfhearts;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
           if (i < health)
           {
               hearts[i].sprite = fullHeart;
           }
           else
           {
               hearts[i].sprite = emptyHeart;
           }
        
        
        //This dictates how many hearts are on the screen at a time, i do not think i need it since im not doing levels
            if (i < numOfhearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
