using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class playerHit : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("sharp"))
        {
            print("HIT");
        }
    }
}
