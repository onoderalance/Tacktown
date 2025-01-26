using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class BossManagerScript : MonoBehaviour
{
    public GameObject projectile;
    public GameObject homingMissile;
    public GameObject rotatingSpike;

    float preciseTimer = 0.0f; //delta time
    int stepCounter = 0; //increases every (stepTime) seconds
    bool stepUpdated = false;
    public float stepsPerSecond = 2.933f; //8th notes at 88bpm

    enum AttackType    {SHOT_FROM_TOP, SHOT_FROM_BOTTOM, SHOT_FROM_LEFT, SHOT_FROM_RIGHT,
                    BURST_FROM_TOP, BURST_FROM_BOTTOM, BURST_FROM_LEFT, BURST_FROM_RIGHT,
                    SHOT_FROM_CENTER, MISSILE_FROM_CENTER};

    Dictionary<int, List<Attack>> attackList; //maps a time to a list of attacks that will happen on this step

    // Start is called before the first frame update
    void Start()
    {

        //projectile = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BossFightEnemies/BossProjecile.prefab", typeof(GameObject));
        //GameObject newProjectile = Instantiate(projectile, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));


        //attackList = new Dictionary<int, List<Attack>>
        //{
        //    [3] = new List<Attack> { (SingleShotFromTop)(new SingleShotFromTop(1.5f, 2.0f, projectile)) },
        //    [6] = new List<Attack> { new SingleShotFromCenter(90.0f, 2.0f, projectile) },
        //    [9] = new List<Attack> { new BurstFromCenter(180.0f, 2.0f, 5, stepCounter, 1, 10.0f, projectile, attackList) },
        //};

        attackList = new Dictionary<int, List<Attack>>();

        attackList[3] = new List<Attack> { new SingleShotFromTop(1.5f, 2.0f, projectile) };
        attackList[6] = new List<Attack> { new SingleShotFromCenter(90.0f, 2.0f, projectile) };

        //downbeat of first chorus (measure 5)
        attackList[33] = new List<Attack> {
            new SingleShotFromTop(6.0f, 1.5f, projectile),
            new SingleShotFromLeft(-6.0f, 1.5f, projectile),
            new SingleShotFromRight(-6.0f, 1.5f, projectile),
            new SingleShotFromBottom(6.0f, 1.5f, projectile)
        };

        //measure 7
        attackList[48] = new List<Attack>();
        //start xPos, speed, numShots, step start, time offset, xPos offset, projectile object, attack list
        new Burst(-3.0f, 2.0f, 4, 57, 1, 0.8f, projectile, attackList,0);

        //measure 7
        attackList[56] = new List<Attack>();
        //start xPos, speed, numShots, step start, time offset, xPos offset, projectile object, attack list
        new Burst(-1.7f, 2.0f, 4, 65, 1, 0.8f, projectile, attackList,1);

        //measure 7
        attackList[64] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(90.0f, 3.5f, 2, 64, 1, 180.0f, projectile, attackList);

        //measure 8
        attackList[72] = new List<Attack> {
            new SingleShotFromTop(-1.25f, 2.0f, projectile),
            new SingleShotFromBottom(9.3f, 2.0f, projectile),
        };

        //measure 9
        attackList[80] = new List<Attack> {
            new SingleShotFromRight(0.0f, 2.0f, projectile),
            new SingleShotFromRight(-10.0f, 2.0f, projectile),
        };

        //big horn hit at step 162
        attackList[162] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(0.0f, 2.0f, 8, 162, 0, 45.0f, projectile, attackList);

    }

    // Update is called once per frame
    void Update()
    {
        preciseTimer += Time.deltaTime;

        //Debug.Log((int)(preciseTimer/stepsPerSecond));

        //call onStepUpdate if we have just moved to a new step
        if ((int)(preciseTimer * stepsPerSecond) != stepCounter)
        {
            stepCounter = (int)(preciseTimer * stepsPerSecond);
            Debug.Log(stepCounter);
            onStepUpdate();
        }

    }

    //only called when the step counter is updated
    void onStepUpdate()
    {

        //if there is an attack on this step:
        if (attackList.ContainsKey(stepCounter))
        {
            //print("CONTAINS KEY");
            List<Attack> currentStepAttackList = attackList[stepCounter]; //a list of attacks happening on this step
            print("current step attack list count: " + currentStepAttackList.Count);
            for (int i = 0; i < currentStepAttackList.Count; i++)
            {
                print("i: " + i);
                currentStepAttackList[i].create();
            }
        }
    }

    public abstract class Attack {

        public abstract void create();


    }

    public class SingleShotFromTop:Attack
    {
        private float xPos;
        private float speed;
        public GameObject projectile;

        public SingleShotFromTop(float xPos, float speed, GameObject projectile)
        {
            this.xPos = xPos;
            this.speed = speed;
            this.projectile = projectile;
        }

        public override void create() {
            float yPos = 1.0f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 0));
            newProjectile.GetComponent<BossProjectileScript>().speed = speed;
        }

    }

    public class SingleShotFromBottom:Attack
    {
        private float xPos;
        private float speed;
        private GameObject projectile;

        public SingleShotFromBottom(float xPos, float speed, GameObject projectile)
        {
            this.xPos = xPos;
            this.speed = speed;
            this.projectile = projectile;
        }

        public override void create()
        {
            float yPos = -15.6f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 180));
            newProjectile.GetComponent<TackProjectileScript>().speed = speed;
        }
    }

    public class SingleShotFromLeft:Attack
    {
        private float yPos;
        private float speed;
        private GameObject projectile;

        public SingleShotFromLeft(float yPos, float speed, GameObject projectile)
        {
            this.yPos = yPos;
            this.speed = speed;
            this.projectile = projectile;
        }

        public override void create()
        {
            float xPos = -6.27f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 90));
            try
            {
                newProjectile.GetComponent<BossProjectileScript>().speed = speed;
            }
            catch
            {
                //do nothing
            }
            
        }
    }

    public class SingleShotFromRight:Attack
    {
        private float yPos;
        private float speed;
        private GameObject projectile;

        public SingleShotFromRight(float yPos, float speed, GameObject projectile)
        {
            this.yPos = yPos;
            this.speed = speed;
            this.projectile = projectile;
        }

        public override void create()
        {
            float xPos = -14.3f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            //GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 270));
            //newProjectile.GetComponent<TackProjectileScript>().speed = speed;
        }
    }

    class SingleShotFromCenter:Attack
    {
        private float angle;
        private float speed;
        private GameObject projectile;

        public SingleShotFromCenter(float angle, float speed, GameObject projectile)
        {
            this.angle = angle;
            this.speed = speed;
            this.projectile = projectile;
        }

        public override void create()
        {
            Vector3 centerPosition = new Vector3(4, -6, 0);
            GameObject newProjectile = Instantiate(projectile, centerPosition, Quaternion.Euler(0, 0, angle));
        }

    }

    class BurstFromCenter:Attack
    {
        private float angle;
        private float speed;
        private GameObject projectile;
        private float timeOffset;
        private float angleOffset;

        public BurstFromCenter(float angle, float speed, int numShots, int stepCounter, int timeOffset, float angleOffset, GameObject projectile, Dictionary<int, List<Attack>> attackList) {

            if (attackList == null)
            {
                throw new ArgumentNullException(nameof(attackList), "The attack list cannot be null.");
            }

            if (projectile == null)
            {
                throw new ArgumentNullException(nameof(projectile), "The projectile cannot be null.");
            }

            if (numShots <= 0)
            {
                throw new ArgumentException("The number of shots must be greater than zero.", nameof(numShots));
            }

            if (timeOffset < 0)
            {
                throw new ArgumentException("Time offset cannot be negative.", nameof(timeOffset));
            }

            Vector3 centerPosition = new Vector3(4, -6, 0);

            //add all burst shots
            for (int i = 0; i < numShots; i++)
            {
                int currentStep = stepCounter + i*timeOffset;
                //if the key exists, append to it. otherwise, create a new list
                SingleShotFromCenter newShot = new SingleShotFromCenter(angle + i * angleOffset, speed, projectile);
                if (attackList.ContainsKey(currentStep))
                {
                    List<Attack> currentAttackList = attackList[currentStep];
                    //print("list for step: " + currentStep + " " + currentAttackList + " length " + currentAttackList.Count);
                    currentAttackList.Add(newShot);
                } else {
                    attackList[currentStep] = new List<Attack> { newShot };
                }

            }
        }

        public override void create() {
            //don't need to do anything here
        }


    }

    class Burst: Attack
    {
        private float xPos;
        private float speed;
        private GameObject projectile;
        private float timeOffset;
        private float xOffset;

        public Burst(float startPos, float speed, int numShots, int stepCounter, int timeOffset, float posOffset, GameObject projectile, Dictionary<int, List<Attack>> attackList, int direction)
        {

            if (attackList == null)
            {
                throw new ArgumentNullException(nameof(attackList), "The attack list cannot be null.");
            }

            if (projectile == null)
            {
                throw new ArgumentNullException(nameof(projectile), "The projectile cannot be null.");
            }

            if (numShots <= 0)
            {
                throw new ArgumentException("The number of shots must be greater than zero.", nameof(numShots));
            }

            if (timeOffset < 0)
            {
                throw new ArgumentException("Time offset cannot be negative.", nameof(timeOffset));
            }

            Vector3 centerPosition = new Vector3(4, -6, 0);

            //add all burst shots
            for (int i = 0; i < numShots; i++)
            {
                int currentStep = stepCounter + i * timeOffset;
                //if the key exists, append to it. otherwise, create a new list
                Attack newShot = new SingleShotFromTop(startPos + i * posOffset, speed, projectile);
                switch (direction)
                {
                    case 0:
                        newShot = new SingleShotFromTop(startPos + i * posOffset, speed, projectile);
                        break;
                    case 1:
                        newShot = new SingleShotFromLeft(startPos + i * posOffset, speed, projectile);
                        break;
                    case 2:
                        newShot = new SingleShotFromBottom(startPos + i * posOffset, speed, projectile);
                        break;
                    case 3:
                        newShot = new SingleShotFromRight(startPos + i * posOffset, speed, projectile);
                        break;

                }
                if (attackList.ContainsKey(currentStep))
                {
                    List<Attack> currentAttackList = attackList[currentStep];
                    currentAttackList.Add(newShot);
                }
                else
                {
                    attackList[currentStep] = new List<Attack> { newShot };
                }

            }
        }

        public override void create()
        {
            //don't need to do anything here
        }
    }




    //fires a single shot at the given x position from the top of the screen downwards
    void singleShotFromTop(float xPos)
    {
        float yPos = -0.5f;
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
        //GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0,0,0));

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
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
        //GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 180));
    }

    void burstFromBottom(float xPos, int numShots, float spaceOffset, float timeOffset)
    {

    }

    //fires a single shot at the given y position from the left of the screen rightwards
    void singleShotFromLeft(float yPos)
    {
        float xPos = -0.5f;
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
        //GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 0));
    }


    void burstFromLeft(float yPos, int numShots, float spaceOffset, float timeOffset)
    {

    }

    void burstFromRight(float yPos, int numShots, float spaceOffset, float timeOffset)
    {

    }

    //SHOTS FROM MIDDLE
    Vector3 centerPosition = new Vector3(4,-6,0);

    void homingMissleFromCenter() {
        //GameObject newProjectile = Instantiate(homingMissile, centerPosition, Quaternion.Euler(0, 0, 0));
    }

}
