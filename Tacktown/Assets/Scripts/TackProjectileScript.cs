using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackProjectileScript : MonoBehaviour
{

    public float speed = 1.0f;
    float rangeFromPlayer = 30.0f;
    //public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
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
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision!");
        if (collision.collider.tag == "wall") {
            Destroy(gameObject);
        }

    }
}
