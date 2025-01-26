using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public AudioClip bubbleAudio;
    public AudioClip tackAudio;
    public AudioSource audioSource;
    public bool useBubbleAudio = true; // choose which audio to use

    public bool isActive = false; //determines if the given text box is active
    public bool canSkip = true; //determiens if the player is able to skip the current text

    private Coroutine typingCoroutine; // Store the current typing coroutine for control

    void Start()
    {
        textComponent.text = string.Empty;
    }

    void Update()
    {
        if (isActive)
        {
            if(canSkip)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) // skip to end with click or space
                {
                    if (textComponent.text == lines[index])
                    {
                        NextLine();
                    }
                    else
                    {
                        StopTyping();
                        textComponent.text = lines[index];
                        StopAudio();
                    }
                }
            }
        } 
    }

    public int getCurrentIndex()
    {
        return index;
    }

    public void StartDialogue()
    {
        index = 0;
        ContinueDialogue();
    }

    public void ContinueDialogue() // dont really need to call
    {
        if (index < lines.Length)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeLine());
        }
        isActive = true;
    }

    public void PauseDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
            StopAudio();
        }
        //clear text
        textComponent.text = string.Empty;
        isActive = false;
    }

    // USE THIS MOSTLY
    public void SkipToLine(int lineIndex) // calls continuedialogue
    {
        if (lineIndex >= 0 && lineIndex < lines.Length)
        {
            PauseDialogue(); // Stop current typing
            index = lineIndex;
            textComponent.text = string.Empty;
            ContinueDialogue();
        }
    }

    public bool toggleSkip() // allows you to disable/able ffwrd / skip, returns whether active or not
    {
        canSkip = !canSkip;
        return canSkip;
    }

    private void StopTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Stop the current typing coroutine
            typingCoroutine = null;
            StopAudio(); // Stop the typing sound
        }
    }

    IEnumerator TypeLine()
    {
        PlayAudio(); // Start audio as typing begins

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        StopAudio(); // Stop when line finishes
        typingCoroutine = null; // Mark typing as finished
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            ContinueDialogue();
        }
        else
        {
            gameObject.SetActive(false); // End dialogue
        }
    }

    private void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.clip = useBubbleAudio ? bubbleAudio : tackAudio;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void changeAudio()
    {
        useBubbleAudio = !useBubbleAudio;
    }
}

