using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float entryTime = 0.0f; //when should it enter the boss fight

    float rangeFromPlayer = 30.0f; //how far will it continue to exist
    private Rigidbody2D rb;
    //public GameObject player;
    int hits = 0; //it can hit a wall once (to enter the arena)

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move it forward
        transform.position += transform.up * Time.deltaTime * -speed;

        //calculate distance from player, destroy if too far away
        //float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        //if (distanceFromPlayer > rangeFromPlayer)
        //{
        //    Destroy(gameObject);
        //}
    }



    //destroy itself when it hits a wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {

            Destroy(gameObject);
        }

    }
}
