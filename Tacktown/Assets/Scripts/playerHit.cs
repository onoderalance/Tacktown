using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHit : MonoBehaviour
{
    public Rigidbody2D rb;
    public gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<gameManager>(); // Find the gameManager in the scene
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("sharp"))
        {
            gm.gameOver();
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                gm.restartGame();
            }
        }
    }
}
