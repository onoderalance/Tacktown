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
    public AudioSource audioSource;

    public SpriteRenderer spriteRenderer;
    public Sprite background;
    public Sprite title;
    public Sprite subtitle;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite3a;
    public Sprite sprite4;

    public int SceneID; //determines what scene we are running!

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
        startScene(SceneID);
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
                    case 0: //blalck 1
                        //start audio
                        audioSource.Play();
                        switchFrame(2, -1);
                        autoTimeToNext(6.04f);
                        break;
                    case 1: //title
                        switchFrame(0, -1);
                        autoTimeToNext(3.12f);
                        break;
                    case 2: //black 1
                        switchFrame(2, -1);
                        autoTimeToNext(2.0f);
                        break;
                    case 3: //subtitle
                        switchFrame(1, -1);
                        autoTimeToNext(4.5f);
                        break;
                    case 4: //black 2
                        switchFrame(2, -1);
                        autoTimeToNext(3.5f);
                        break;
                    case 5: //it always starts...
                        switchFrame(3, 0);
                        autoTimeToNext(5.0f);
                        break;
                    case 6: //My woman...
                        switchFrame(4, 0);
                        autoTimeToNext(3.0f);
                        startFade(1.5f, false);
                        break;
                    case 7: //She was my belle.
                        switchFrame(4, 1);
                        autoTimeToNext(2.0f);
                        startFade(1.5f, true);
                        break;
                    case 8: //Soft eyes...
                        switchFrame(5, 2);
                        autoTimeToNext(4.0f);
                        startFade(1.0f, true);
                        break;
                    case 9: //She carried
                        switchFrame(4, 3);
                        autoTimeToNext(2.6f);
                        startFade(1.5f, false);
                        break;
                    case 10: //Gorg
                        switchFrame(4, 4);
                        autoTimeToNext(1.0f);
                        break;
                    case 11: //Gase
                        switchFrame(4, 5);
                        autoTimeToNext(1.0f);
                        break;
                    case 12: //Bod
                        switchFrame(4, 6);
                        autoTimeToNext(1.0f);
                        break;
                    case 13: //No matter....
                        switchFrame(5, 7);
                        autoTimeToNext(4.0f);
                        startFade(2.4f, true);
                        break;
                    case 14: //When the surface...
                        setFadeAlpha(1.0f);
                        switchFrame(4, 8);
                        autoTimeToNext(2.5f);
                        break;
                    case 15: //When the air runs
                        switchFrame(4, 9);
                        autoTimeToNext(2.5f);
                        break;
                    case 16: //And I messed up
                        switchFrame(3, 1);
                        autoTimeToNext(3.5f);
                        break;
                    case 17: //I thought I was protecting her
                        switchFrame(3, 2);
                        autoTimeToNext(4.0f);
                        break;
                    case 18: //but i hurt....
                        switchFrame(3, 3);
                        autoTimeToNext(4.0f);
                        break;
                    case 19: //And just like that...
                        switchFrame(5, 10);
                        autoTimeToNext(4.0f);
                        startFade(2.4f, true);
                        break;
                    case 20: //But now....
                        switchFrame(4, 11);
                        autoTimeToNext(3.0f);
                        startFade(1.5f, false);
                        break;
                    case 21: //Snatched..
                        switchFrame(4, 12);
                        autoTimeToNext(3.0f);
                        break;
                    case 22: //Word is...
                        switchFrame(4, 13);
                        autoTimeToNext(4.0f);
                        break;
                    case 23: //...the sketchiest
                        switchFrame(4, 14);
                        autoTimeToNext(3.0f);
                        break;
                    case 24: //This town has
                        switchFrame(4, 15);
                        autoTimeToNext(3.0f);
                        break;
                    case 25: //The air isi sharp
                        switchFrame(4, 16);
                        autoTimeToNext(3.0f);
                        break;
                    case 26: //Its not pretty
                        switchFrame(4, 17);
                        autoTimeToNext(2.5f);
                        break;
                    case 27: //Its not clean
                        switchFrame(4, 18);
                        autoTimeToNext(2.5f);
                        break;
                    case 28: //But I at least
                        switchFrame(4, 19);
                        autoTimeToNext(3.0f);
                        startFade(2.0f, true);
                        break;
                    case 29: //they say
                        switchFrame(3, 4);
                        autoTimeToNext(4f);
                        break;
                    case 30: //but i sure as hell
                        switchFrame(3, 5);
                        autoTimeToNext(3.5f);
                        break;

                }
                break;
            case 2:
                switch (cutsceneIndex)
                {
                    case 0: //blalck 1
                        startFade(2.0f, false);
                        switchFrame(2, -1);
                        autoTimeToNext(0.5f);
                        break;
                    case 1: //Michelle!
                        switchFrame(7, 20);
                        autoTimeToNext(4.0f);
                        break;
                    case 2: //Lost me?
                        switchFrame(7, 21);
                        autoTimeToNext(2.0f);
                        break;
                    case 3: //Aww...
                        switchFrame(7, 22);
                        startFade(2.0f, true);
                        autoTimeToNext(4.0f);
                        break;
                    case 4: //You always
                        switchFrame(3, 6);
                        autoTimeToNext(5.5f);
                        break;
                    case 5: //Im done
                        switchFrame(6, 23);
                        startFade(2.0f, false);
                        autoTimeToNext(3.0f);
                        break;
                    case 6: //pop him
                        switchFrame(6, 24);
                        autoTimeToNext(4.0f);
                        break;

                }
                break;
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
    // frame as -1 when no dialogue is given
    void switchFrame(int scene, int frame)
    {
        switch (scene)
        {
            case 0: //title
                spriteRenderer.sprite = title;
                hudObject.SetActive(false);
                dialogueMain.PauseDialogue();
                dialogueCenter.PauseDialogue();
                break;
            case 1: //subtitle
                spriteRenderer.sprite = subtitle;
                hudObject.SetActive(false);
                dialogueMain.PauseDialogue();
                dialogueCenter.PauseDialogue();
                break;
            case 2: //background blank
                spriteRenderer.sprite = background;
                hudObject.SetActive(false);
                dialogueMain.PauseDialogue();
                dialogueCenter.PauseDialogue();
                break;
            case 3: //background with text
                spriteRenderer.sprite = background;
                hudObject.SetActive(false);
                dialogueCenter.SkipToLine(frame);
                dialogueMain.PauseDialogue();
                currDialogue = dialogueMain;
                break;
            case 4: //sprite 1
                spriteRenderer.sprite = sprite1;
                hudObject.SetActive(true);
                dialogueCenter.PauseDialogue();
                dialogueMain.SkipToLine(frame);
                currDialogue = dialogueCenter;
                break;
            case 5: //sprite 2
                spriteRenderer.sprite = sprite2;
                hudObject.SetActive(true);
                dialogueCenter.PauseDialogue();
                dialogueMain.SkipToLine(frame);
                currDialogue = dialogueCenter;
                break;
            case 6: //sprite 3
                spriteRenderer.sprite = sprite3;
                hudObject.SetActive(true);
                dialogueCenter.PauseDialogue();
                dialogueMain.SkipToLine(frame);
                currDialogue = dialogueCenter;
                break;
            case 7: //sprite 3a
                spriteRenderer.sprite = sprite3a;
                hudObject.SetActive(true);
                dialogueCenter.PauseDialogue();
                dialogueMain.SkipToLine(frame);
                currDialogue = dialogueCenter;
                break;
            case 8: //sprite 4
                spriteRenderer.sprite = sprite4;
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
