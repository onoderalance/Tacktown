using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneManager : MonoBehaviour
{
    public GameObject hudObject;

    public SpriteRenderer spriteRenderer;
    public Sprite background;
    public Sprite sprite1;

    int cutsceneIndex = -1; //tracks what part of the cutscene we are at
    bool cutsceneNextReady = false; //tracks if we are ready to move on to the next part of the cutscene
    float timeNext = 0.0f; //tracks what time we will be ready for the next portion of the cutscene

    public float timeElapsed = 0.0f;
    public bool timerActive = false;
     
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // increments timer if timer is active
        if (timerActive)
        {
            timeElapsed += Time.deltaTime;
        }

        if (cutsceneIndex >= 0)
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchFrame(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchFrame(2);
        }
    }

    // Called to start running a given cutscene
    // 1 - intro 2 - betrayal 3 - bad end 4 - good end
    public void startScene(int scene)
    {
        //reset index to playing
        cutsceneIndex = 0;
    }

    // Switch visual frame shown for the cutscenes
    void switchFrame(int frame)
    {
        // reactivate hud if necessary
        hudObject.SetActive(true);
  
        switch(frame)
        {
            case 1: //background case, hud should be hidden here
                spriteRenderer.sprite = background;
                hudObject.SetActive(false);
                break;
            case 2:
                spriteRenderer.sprite = sprite1;
                break;
        }
    }

    void resetTimer()
    {
        timerActive = false;
        timeElapsed = 0.0f;
    }

   

}
