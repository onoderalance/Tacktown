using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileScript : MonoBehaviour
{

    public float speed = 1.0f;
    public Transform playerPosition;
    public float timeAlive = 5.0f;

    float rangeFromPlayer = 30.0f;
    float timer = 0.0f;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //update timer
        timer += Time.deltaTime;

        //look at player
        transform.LookAt(playerPosition);

        //move it forward
        transform.position += transform.up * Time.deltaTime * -speed;
    }

    void trackTimeAlive() {
        //see if timer has expired. If it has, initiate self destruct sequence
        if (timer > timeAlive)
        {
            //flash colors
            if ((int)(timer*2) % 2 == 0)
            {
                sprite.color = new Color(1, 0, 0);
            }
            
        }
    }

}
