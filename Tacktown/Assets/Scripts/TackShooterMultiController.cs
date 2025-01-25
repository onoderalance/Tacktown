using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackShooterMultiController : MonoBehaviour
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
            spawnProjectiles(shootSpeed);
            shotTimer = 0.0f;
        }
    }

    //spawn 3 projectiles in different directions
    void spawnProjectiles(float speed)
    {

        Vector3 spawnPosition1 = (transform.position + new Vector3(0, spawnOffset, 0));
        GameObject newProjectile1 = Instantiate(projectile, transform.position + transform.up * spawnOffset, transform.rotation);
        newProjectile1.GetComponent<TackProjectileScript>().speed = speed;

        //determine offset position of this projectile
        Vector3 spawnPosition2 = transform.position + new Vector3(-spawnOffset, 0, 0);
        //determine rotation of 2nd projectile
        Quaternion rotationProjectile2 = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0,0,90));
        GameObject newProjectile2 = Instantiate(projectile, transform.position - transform.right * spawnOffset, rotationProjectile2);
        newProjectile2.GetComponent<TackProjectileScript>().speed = speed;

        Vector3 spawnPosition3 = transform.position + new Vector3(spawnOffset, 0, 0);
        Quaternion rotationProjectile3 = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, -90));
        GameObject newProjectile3 = Instantiate(projectile, transform.position + transform.right * spawnOffset, rotationProjectile3);
        newProjectile3.GetComponent<TackProjectileScript>().speed = speed;
    }

}
