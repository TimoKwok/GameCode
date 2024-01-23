using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    enum DialogueState
    {
        Idle,
        Greeting,
        Phrase
    }

    //[Header("Visual Cue")]
    //[SerializeField] private GameObject visualCue;
    bool playerDetection = false;

    public GameObject DialogueBox;
    public TextMeshProUGUI TextBox;
    public string[] greetings; // Changed variable name to plural
    public string[] phrases; // Added phrases array
    public float wordSpeed;

    public GameObject InteractPrompt;

    private Coroutine typingCoroutine;
    private DialogueState currentDialogueState = DialogueState.Idle;

    void Start()
    {
        //visualCue.SetActive(false);
        TextBox.text = "";
    }

    void Update()
    {
        if (playerDetection)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                InteractPrompt.SetActive(false);
                if (currentDialogueState == DialogueState.Phrase)
                {
                    zeroText();
                }
                else
                {
                    DialogueBox.SetActive(!DialogueBox.activeInHierarchy);
                    StartTypingCoroutine(greetings[Random.Range(0, greetings.Length)], true);
                    currentDialogueState = DialogueState.Greeting;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && DialogueBox.activeInHierarchy)
            {
                if (currentDialogueState == DialogueState.Greeting)
                {
                    StopTypingCoroutine();
                    currentDialogueState = DialogueState.Phrase;
                    NextLine();
                }
                else if (currentDialogueState == DialogueState.Phrase)
                {
                    zeroText();
                }
            }
        }
    }

    public void zeroText()
    {
        TextBox.text = "";
        DialogueBox.SetActive(false);
        StopTypingCoroutine();
        currentDialogueState = DialogueState.Idle;
    }

    private void StartTypingCoroutine(string text, bool isGreeting)
    {
        StopTypingCoroutine(); // Stop existing coroutine before starting a new one
        typingCoroutine = StartCoroutine(Typing(text, isGreeting));
    }

    private void StopTypingCoroutine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
    }

    IEnumerator Typing(string text, bool isGreeting)
    {
        foreach (char letter in text.ToCharArray())
        {
            TextBox.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        if (!isGreeting) // Check if it's not a greeting, then add a delay before clearing text
        {
            yield return new WaitForSeconds(1.0f); // You can adjust the delay duration
            zeroText();
        }
    }

    public void NextLine()
    {
        string selectedPhrase = phrases[Random.Range(0, phrases.Length)]; // Randomly select a phrase
        StartTypingCoroutine(selectedPhrase, false); // Pass false to indicate it's not a greeting
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
        zeroText();
    }
}