using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileScript : MonoBehaviour
{
    public float speed = 1.0f;

    float rangeFromPlayer = 30.0f; //how far will it continue to exist
    private Rigidbody2D rb;
    //public GameObject player;
    int hits = 2; //it can hit a wall once (to enter the arena)
    float timer = 0.0f;
    float timeAlive = 12.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        //move it forward
        transform.position += transform.up * Time.deltaTime * -speed;

        //calculate distance from player, destroy if too far away
        //float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        //if (distanceFromPlayer > rangeFromPlayer)
        //{
        //    Destroy(gameObject);
        //}

        if (timer > timeAlive)
        {
            Destroy(gameObject);

        }
    }



    //destroy itself when it hits a wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            print("collision hits down");
            hits--;
            if (hits == 0)
            {
                print("DESTROY");
                //Destroy(gameObject);
            }
        }
    }
}
