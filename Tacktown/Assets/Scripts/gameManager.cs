using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public movement m;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartGame()
    {
        print("RESTART CLICKED");
        SceneManager.LoadScene("Level");
        gameOverScreen.SetActive(false);
        m.bubleIsAlive = true;
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void mainToPreBoss()
    {
        print("GOAL REACHED");
        SceneManager.LoadScene("CutsceneBetray");
    }
}
