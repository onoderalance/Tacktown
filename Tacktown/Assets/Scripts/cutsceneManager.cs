using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cutsceneManager : MonoBehaviour
{
    public GameObject hudObject;
    public dialogue dialogueMain;
    public dialogue dialogueCenter;

    public SpriteRenderer spriteRenderer;
    public Sprite background;
    public Sprite sprite1;

    int cutsceneIndex = -1; //tracks what part of the cutscene we are at
    int currScene = 0; //tracks currently running scene
    bool cutsceneNextReady = false; //tracks if we are ready to move on to the next part of the cutscene
   
    public dialogue currDialogue; //tracks which dialogue object is currently being used
    int nextSceneIndex = -1; //determines what text index will be the next part of the scene

    float timeNext = 0.0f; //tracks what time we will be ready for the next portion of the cutscene
    public float timeElapsed = 0.0f;
    public bool timerActive = false;
     
    // Start is called before the first frame update
    void Start()
    {
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
            print(cutsceneNextReady);
            print(timerActive);
            print(timeElapsed);
            print(timeNext);
            // next scene is ready and timer is elapsed or not initialized
            if (cutsceneNextReady && timeElapsed >= timeNext)
            {
                print("baba");
                resetTimer();
                sceneCheck();
                cutsceneIndex += 1;
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
                        nextSceneIndex = 1; //next index is 1
                        cutsceneNextReady = true; //auto scrolling
                        timerActive = true;
                        timeNext = 5.0f;
                        break;
                    case 1: //My woman...
                        switchFrame(2 , 0);
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
        }
    }

    //resets the active timer
    void resetTimer()
    {
        timerActive = false;
        timeElapsed = 0.0f;
    }


   

}
