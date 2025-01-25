using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackShooterController : MonoBehaviour
{

    public float shootRate = 3.0f;
    public float shootSpeed = 1.0f;
    public GameObject projectile;
    public float spawnOffset = -1.0f;
    float shotTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer >= shootRate)
        {
            spawnProjectile(shootSpeed);
            shotTimer = 0.0f;
        }
    }

    void spawnProjectile(float speed) {

        Vector3 spawnPosition = transform.position + new Vector3(0,spawnOffset,0);

        GameObject newProjectile = Instantiate(projectile, spawnPosition, transform.rotation);
        newProjectile.GetComponent<TackProjectileScript>().speed = speed;
    }

}
