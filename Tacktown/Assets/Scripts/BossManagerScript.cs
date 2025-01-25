using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagerScript : MonoBehaviour
{
    public GameObject projectile;
    public GameObject homingMissile;
    public GameObject rotatingSpike;

    float preciseTimer = 0.0f; //delta time
    int stepCounter = 0; //increases every (stepTime) seconds
    bool stepUpdated = false;
    public float stepTime = 1.0f;

    enum AttackType    {SHOT_FROM_TOP, SHOT_FROM_BOTTOM, SHOT_FROM_LEFT, SHOT_FROM_RIGHT,
                    BURST_FROM_TOP, BURST_FROM_BOTTOM, BURST_FROM_LEFT, BURST_FROM_RIGHT,
                    SHOT_FROM_CENTER, MISSILE_FROM_CENTER};

    private class Attack {

        public void create() {
            //TO BE OVERRIDDEN!
            //can be called by any attack child to add it to the game world
        }


    }

    private class SingleShotFromTop
    {
        private float xPos;
        private float speed;

        public ShotFromTop(float xPos)
        {

        }
    }


    struct Attack {
        AttackType type;
        
    }


    public Dictionary<int, List<Attack> attackList; //maps a time to a list of attacks that will happen on this step

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //only called when the step counter is updated
    void onStepUpdate() {

        

        stepUpdated = false;
    }



    //fires a single shot at the given x position from the top of the screen downwards
    void singleShotFromTop(float xPos)
    {
        float yPos = -0.5f;
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0)
        GameObject newProjectile = Instantiate(projectile, spawnPosition, new Vector3(0,0,0));

    }

    //fires several shots down from the top of the screen, starting at xPos
    //if timeOffset is a positive value, these are shot one after another,
	//with timeOffset representing the time between each shot
    void burstFromTop(float xPos, int numShots, float spaceOffset, float timeOffset) {

    }

    //fires a single shot at the given x position from the bottom of the screen upwards
    void singleShotFromBottom(float xPos)
    {
        float yPos = -1.78f;
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0)
        GameObject newProjectile = Instantiate(projectile, spawnPosition, new Vector3(0, 0, 180));
    }

    void burstFromBottom(float xPos, int numShots, float spaceOffset, float timeOffset)
    {

    }

    //fires a single shot at the given y position from the left of the screen rightwards
    void singleShotFromLeft(float yPos)
    {
        float yPos = -0.5f;
        Vector3 spawnPosition = new Vector3(xPos, -0.5, 0)
        GameObject newProjectile = Instantiate(projectile, spawnPosition, new Vector3(0, 0, 0));
    }


    void burstFromLeft(float yPos, int numShots, float spaceOffset, float timeOffset)
    {

    }

    void burstFromRight(float yPos, int numShots, float spaceOffset, float timeOffset)
    {

    }

    //SHOTS FROM MIDDLE
    Vector3 centerPosition = new Vector3(4,-6,0);

    void singleShotFromCenter(float angle)
    {
        GameObject newProjectile = Instantiate(projectile, centerPosition, new Vector3(0, 0, angle));
    }

    void homingMissleFromCenter() {
        GameObject newProjectile = Instantiate(homingMissile, centerPosition, new Vector3(0, 0, 0));
    }

}
