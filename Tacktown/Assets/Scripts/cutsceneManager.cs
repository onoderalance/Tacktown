using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cutsceneManager : MonoBehaviour
{
    public GameObject hudObject;
    public dialogue dialogueMain;
    public dialogue dialogueCenter;
    public GameObject fadeBlock;

    public SpriteRenderer spriteRenderer;
    public Sprite background;
    public Sprite sprite1;
    public Sprite sprite2;

    int cutsceneIndex = -1; //tracks what part of the cutscene we are at
    int currScene = 0; //tracks currently running scene
    bool cutsceneNextReady = false; //tracks if we are ready to move on to the next part of the cutscene
   
    public dialogue currDialogue; //tracks which dialogue object is currently being used
    int nextSceneIndex = -1; //determines what text index will be the next part of the scene

    float timeNext = 0.0f; //tracks what time we will be ready for the next portion of the cutscene
    public float timeElapsed = 0.0f;
    public bool timerActive = false;

    public float fadeTimeElapsed = 0.0f;
    float fadeDuration = 0.0f;
    bool fadeActive = false;
    bool fadeDir = false;
    

     
    // Start is called before the first frame update
    void Start()
    {
        setFadeAlpha(0.0f);
        dialogueMain.PauseDialogue();
        dialogueCenter.PauseDialogue();
        dialogueMain.toggleSkip();
        dialogueCenter.toggleSkip();
        resetTimer();
        startScene(1);
        sceneCheck();
    }

    // Update is called once per frame
    void Update()
    {
        // increments timer if timer is active
        if (timerActive)
        {
            timeElapsed += Time.deltaTime;
        }
        //fades if fade timer active
        if (fadeActive)
        {
            fadeTimeElapsed += Time.deltaTime;

            // Calculate the step per frame based on fadeDuration
            float increment = Time.deltaTime / fadeDuration; // Increment based on time per frame

            float currentAlpha = getFadeAlpha();

            // Fade in
            if (fadeDir)
            {
                setFadeAlpha(Mathf.Clamp01(currentAlpha + increment)); // Increment alpha smoothly
            }
            // Fade out
            else
            {
                setFadeAlpha(Mathf.Clamp01(currentAlpha - increment)); // Decrement alpha smoothly
            }

            // End fade when time is up
            if (fadeTimeElapsed >= fadeDuration)
            {
                endFade();
            }


        }
        // increments cutsceneIndex if conditions are met
        if (cutsceneIndex >= 0)
        {
            // text has gone far enough to trigger the next scene
            if(currDialogue != null)
            {
                if (currDialogue.getCurrentIndex() == nextSceneIndex)
                {
                    cutsceneNextReady = true;
                }
            }
            // next scene is ready and timer is elapsed or not initialized
            if (cutsceneNextReady && timeElapsed >= timeNext)
            {
                //print("baba");
                cutsceneIndex += 1;
                cutsceneNextReady = false;
                resetTimer();
                sceneCheck();
            }
        }
    }

    // Called to start running a given cutscene
    // 1 - intro 2 - betrayal 3 - bad end 4 - good end
    public void startScene(int scene)
    {
        //reset index to playing
        cutsceneIndex = 0;
        currScene = scene;
    }

    // Determines what is going on in the current scene
    void sceneCheck()
    {
        switch (currScene)
        {
            case 1: //intro scene
                switch (cutsceneIndex)
                { 
                    case 0: //it always starts...
                        switchFrame(1, 0);
                        autoTimeToNext(5.0f);
                        break;
                    case 1: //My woman...
                        switchFrame(2, 0);
                        autoTimeToNext(3.0f);
                        break;
                    case 2: //She was my belle.
                        switchFrame(2, 1);
                        autoTimeToNext(2.0f);
                        startFade(1.5f, true);
                        break;
                    case 3: //It's not going to be pretty...
                        switchFrame(3, 2);
                        autoTimeToNext(3.0f);
                        //setFadeAlpha(0.0f);
                        startFade(1.5f, true);
                        break;

                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
   
    //used to reset timer when the scene needs to autoplay to the next point
    void autoTimeToNext(float time)
    {
        cutsceneNextReady = true; //auto scrolling
        timerActive = true;
        timeNext = time;
    }

    // Switch visual frame shown for the cutscenes, starting dialogue from given index
    void switchFrame(int scene, int frame)
    {
        switch (scene)
        {
            case 1: //background case, hud should be hidden here
                spriteRenderer.sprite = background;
                hudObject.SetActive(false);
                dialogueCenter.SkipToLine(frame);
                dialogueMain.PauseDialogue();
                currDialogue = dialogueMain;
                break;
            case 2:
                spriteRenderer.sprite = sprite1;
                hudObject.SetActive(true);
                dialogueCenter.PauseDialogue();
                dialogueMain.SkipToLine(frame);
                currDialogue = dialogueCenter;
                break;
            case 3:
                spriteRenderer.sprite = sprite2;
                hudObject.SetActive(true);
                dialogueCenter.PauseDialogue();
                dialogueMain.SkipToLine(frame);
                currDialogue = dialogueCenter;
                break;
        }
    }

    //resets the active timer
    void resetTimer()
    {
        timerActive = false;
        timeElapsed = 0.0f;
    }

    //sets the alpha of the fade block
    void setFadeAlpha(float alpha)
    {
        SpriteRenderer spriteRenderer = fadeBlock.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    //gets the alpha of the fade block
    float getFadeAlpha()
    {
        SpriteRenderer spriteRenderer = fadeBlock.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        return color.a;
    }

    //starts the fade over a given amount of time, dir controls whether is fades in or out
    void startFade(float fadeTime, bool dir)
    {
        // fade in
        if (dir)
        {
            setFadeAlpha(0.0f);
            fadeActive = true;
            fadeDuration = fadeTime;
            fadeDir = dir;
        }
        // fade out
        else
        {
            setFadeAlpha(1.0f);
            fadeActive = true;
            fadeDuration = fadeTime;
            fadeDir = dir;
        }
    }

    //ends the fade whopee
    void endFade()
    {
        fadeActive = false;
        fadeTimeElapsed = 0.0f;
        if (fadeDir)
        {
            setFadeAlpha(1.0f);
        }
        else
        {
            setFadeAlpha(0.0f);
        }
    }
}
