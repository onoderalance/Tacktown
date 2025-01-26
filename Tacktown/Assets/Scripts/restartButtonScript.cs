using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartButtonScript : MonoBehaviour
{
    public GameObject gameOverScreen;

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartButtonClicked()
    {
        print("Restart Button Clicked");
        SceneManager.LoadScene("Level");
        gameOverScreen.SetActive(false);
    }
}
