using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileScript : MonoBehaviour
{

    public float speed = 1.0f;
    public Transform playerPosition;
    public float timeAlive = 5.0f;


    float detonationTime = 2.0f;
    float rangeFromPlayer = 30.0f;
    float timer = 0.0f;
    public int hits = 1;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //update timer
        timer += Time.deltaTime;

        //look at player
        transform.up = playerPosition.position - transform.position;

        //move it forward
        transform.position += transform.up * Time.deltaTime * speed;

        //countdown to explosion
        trackTimeAlive();
    }

    void trackTimeAlive() {
        //see if timer has expired. If it has, initiate self destruct sequence
        if (timer > timeAlive)
        {
            sprite.color = Color.red;
            //flash colors
            if ((int)(timer*2) % 2 == 0)
            {
                sprite.color = Color.red;
            } else
            {
                sprite.color = Color.white;
            }
            
        }

        //if it has been detonating for long enough, delete itself
        if (timer > timeAlive + detonationTime)
        {
            hits--;
            if (hits == 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
