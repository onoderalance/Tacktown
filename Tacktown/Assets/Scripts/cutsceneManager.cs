using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneManager : MonoBehaviour
{
    bool hudState = true;
    public GameObject hudObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleHUD();
        }
        hudObject.SetActive(hudState);
    }

    // Called when running a given cutscene
    // 1 - intro 2 - betrayal 3 - bad end 4 - good end
    void runScene(int scene)
    {

    }

    // Switch visual frame shown for the cutscenes
    void switchFrame(int frame)
    {

    }

    // Toggle HUD bar visual
    void toggleHUD()
    {
        hudState = !hudState; 
    }

    // Set HUD bar visual
    void setHUD(bool hud)
    {
        hudState = hud;
    }

}
