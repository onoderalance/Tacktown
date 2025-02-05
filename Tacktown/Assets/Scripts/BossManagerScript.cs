using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;

public class BossManagerScript : MonoBehaviour
{
    public GameObject projectile;
    public GameObject homingMissile;
    public GameObject rotatingSpike;

    float preciseTimer = 0.0f; //delta time
    int stepCounter = 0; //increases every (stepTime) second
    float stepsPerSecond = (60.0f/88.0f)*2.0f; //8th notes at 88bpm
    float stepDuration = (60.0f/88.0f)/2.0f;
    float timeLeftInStep;
    

    enum AttackType    {SHOT_FROM_TOP, SHOT_FROM_BOTTOM, SHOT_FROM_LEFT, SHOT_FROM_RIGHT,
                    BURST_FROM_TOP, BURST_FROM_BOTTOM, BURST_FROM_LEFT, BURST_FROM_RIGHT,
                    SHOT_FROM_CENTER, MISSILE_FROM_CENTER};

    Dictionary<int, List<Attack>> attackList; //maps a time to a list of attacks that will happen on this step

    // Start is called before the first frame update
    void Start()
    {

        timeLeftInStep = stepDuration;

        //projectile = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BossFightEnemies/BossProjecile.prefab", typeof(GameObject));
        //GameObject newProjectile = Instantiate(projectile, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));


        //attackList = new Dictionary<int, List<Attack>>
        //{
        //    [3] = new List<Attack> { (SingleShotFromTop)(new SingleShotFromTop(1.5f, 2.0f, projectile)) },
        //    [6] = new List<Attack> { new SingleShotFromCenter(90.0f, 2.0f, projectile) },
        //    [9] = new List<Attack> { new BurstFromCenter(180.0f, 2.0f, 5, stepCounter, 1, 10.0f, projectile, attackList) },
        //};

        attackList = new Dictionary<int, List<Attack>>();

        attackList[9] = new List<Attack> { new SingleShotFromCenter(1.5f, 2.0f, projectile) };

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

        //measure 8
        attackList[56] = new List<Attack>();
        //start xPos, speed, numShots, step start, time offset, xPos offset, projectile object, attack list
        new Burst(-1.7f, 2.0f, 4, 65, 1, 0.8f, projectile, attackList,1);

        //measure 9
        attackList[64] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(90.0f, 3.5f, 2, 64, 1, 180.0f, projectile, attackList);

        //measure 10
        attackList[72] = new List<Attack> {
            new SingleShotFromTop(-1.25f, 2.0f, projectile),
            new SingleShotFromBottom(9.3f, 2.0f, projectile),
        };

        //measure 11
        attackList[80] = new List<Attack> {
            new SingleShotFromRight(0.0f, 2.0f, projectile),
            new SingleShotFromRight(-10.0f, 2.0f, projectile),
        };

        //measure 13
        attackList[96] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(0.0f, 2.0f, 3, 96, 0, 45.0f, projectile, attackList);
        new BurstFromCenter(180.0f, 2.0f, 3, 96, 0, 45.0f, projectile, attackList);

        //PRECHORUS
        //measure 15
        attackList[112] = new List<Attack> { new SingleShotFromCenter(1.5f, 2.0f, homingMissile) };

        //measure 17
        attackList[128] = new List<Attack> { new SingleShotFromCenter(1.5f, 2.0f, homingMissile) };

        //CHORUS
        //big horn hit at step 152
        attackList[152] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(0.0f, 3.0f, 8, 152, 0, 45.0f, projectile, attackList);

        //measure 20 horn hits
        attackList[170] = new List<Attack>();
        new Burst(-3.6f, 2.0f, 3, 170, 2, 6.0f, projectile, attackList, 0);

        //measure 21 horn hits
        attackList[178] = new List<Attack>();
        new Burst(-12.0f, 2.0f, 3, 178, 2, 6.0f, projectile, attackList, 3);

        //measure 22
        attackList[184] = new List<Attack>();
        new BurstFromCenter(0.0f, 2.0f, 3, 184, 1, 45.0f, homingMissile, attackList);

        //measure 23
        attackList[192] = new List<Attack>();
        new BurstFromCenter(45.0f, 2.0f, 4, 192, 1, 90.0f, projectile, attackList);

        //measure 24
        attackList[200] = new List<Attack>();
        new Burst(9.0f, 2.0f, 3, 200, 2, -6.0f, projectile, attackList, 2);

        //measure 25
        attackList[208] = new List<Attack>();
        new Burst(-3.6f, 2.0f, 4, 208, 2, 6.0f, projectile, attackList, 0);

        //measure 26 horn hits
        attackList[216] = new List<Attack>();
        new Burst(-12.0f, 2.0f, 3, 216, 2, 6.0f, projectile, attackList, 3);

        //measure 27
        attackList[224] = new List<Attack>();
        new BurstFromCenter(0.0f, 2.0f, 3, 224, 1, 45.0f, homingMissile, attackList);

        //measure 28
        attackList[232] = new List<Attack>();
        new BurstFromCenter(45.0f, 2.0f, 4, 232, 1, 90.0f, projectile, attackList);

        //measure 29
        attackList[240] = new List<Attack>();
        new Burst(9.0f, 2.0f, 3, 240, 2, -6.0f, projectile, attackList, 2);

        //mesaure 32
        attackList[256] = new List<Attack>();
        new Burst(9.0f, 2.0f, 3, 256, 2, -6.0f, projectile, attackList, 3);

        //VERSE
        //measure 36
        attackList[280] = new List<Attack>();
        //start xPos, speed, numShots, step start, time offset, xPos offset, projectile object, attack list
        new Burst(-3.0f, 3.0f, 4, 280, 1, 0.8f, projectile, attackList, 0);

        //measure 37
        attackList[288] = new List<Attack>();
        //start xPos, speed, numShots, step start, time offset, xPos offset, projectile object, attack list
        new Burst(-1.7f, 2.0f, 4, 288, 1, 0.8f, projectile, attackList, 1);

        //measure 38
        attackList[296] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(90.0f, 3.5f, 2, 296, 1, 180.0f, projectile, attackList);

        //measure 39
        attackList[304] = new List<Attack> {
            new SingleShotFromTop(-1.25f, 3.0f, homingMissile),
            new SingleShotFromBottom(9.3f, 3.0f, projectile),
        };

        //measure 40
        attackList[312] = new List<Attack> {
            new SingleShotFromRight(0.0f, 3.0f, projectile),
            new SingleShotFromRight(-10.0f, 3.0f, homingMissile),
        };

        //measure 41
        attackList[320] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(0.0f, 2.0f, 3, 320, 0, 45.0f, projectile, attackList);
        new BurstFromCenter(180.0f, 2.0f, 3, 320, 0, 45.0f, projectile, attackList);

        //measure 42
        attackList[328] = new List<Attack>();
        //start xPos, speed, numShots, step start, time offset, xPos offset, projectile object, attack list
        new Burst(-3.0f, 3.0f, 4, 328, 1, 0.8f, projectile, attackList, 0);

        //measure 43
        attackList[336] = new List<Attack>();
        //start xPos, speed, numShots, step start, time offset, xPos offset, projectile object, attack list
        new Burst(-1.7f, 2.5f, 4, 336, 2, 0.8f, projectile, attackList, 1);

        //measure 44
        attackList[342] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(90.0f, 3.5f, 2, 342, 1, 180.0f, projectile, attackList);

        //measure 45
        attackList[350] = new List<Attack> {
            new SingleShotFromTop(0f, 3.0f, homingMissile),
            new SingleShotFromBottom(7.2f, 3.0f, projectile),
        };

        //measure 46
        attackList[358] = new List<Attack> {
            new SingleShotFromRight(0.0f, 3.0f, projectile),
            new SingleShotFromRight(-10.0f, 3.0f, homingMissile),
        };

        //measure 47
        attackList[366] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(0.0f, 2.0f, 4, 366, 0, 45.0f, projectile, attackList);
        new BurstFromCenter(180.0f, 2.0f, 4, 320, 0, 45.0f, projectile, attackList);




        //CHORUS
        //measure 51
        attackList[400] = new List<Attack>();
        //start angle, speed, numShots, step start, time offset, angle offset, projectile object, attack list
        new BurstFromCenter(0.0f, 3.0f, 8, 400, 0, 45.0f, projectile, attackList);

        //measure 52 horn hits
        attackList[408] = new List<Attack>();
        new Burst(-3.6f, 2.0f, 3, 508, 2, 6.0f, projectile, attackList, 0);

        //measure 53 horn hits
        attackList[416] = new List<Attack>();
        new Burst(-12.0f, 2.0f, 3, 516, 2, 6.0f, projectile, attackList, 3);

        //measure 54
        attackList[424] = new List<Attack>();
        new BurstFromCenter(0.0f, 2.0f, 3, 524, 1, 45.0f, homingMissile, attackList);

        //measure 55
        attackList[432] = new List<Attack>();
        new BurstFromCenter(45.0f, 2.0f, 4, 532, 1, 90.0f, projectile, attackList);

        //measure 56
        attackList[440] = new List<Attack>();
        new Burst(9.0f, 2.0f, 3, 540, 2, -6.0f, projectile, attackList, 2);

        //measure 57
        attackList[448] = new List<Attack>();
        new BurstFromCenter(0.0f, 2.0f, 8, 548, 4, 40.0f, projectile, attackList);

        //measure 60
        attackList[472] = new List<Attack> {
            new SingleShotFromBottom(0.0f, 3.0f, homingMissile),
        };

    }

    // Update is called once per frame
    void Update()
    {
        preciseTimer += Time.deltaTime;
        timeLeftInStep -= Time.deltaTime;

        //Debug.Log((int)(preciseTimer/stepsPerSecond));

        //float stepDuration = 1.0f / stepsPerSecond;
        float stepFloat = preciseTimer*stepsPerSecond;
        //print(stepFloat);

        //call onStepUpdate if we have just moved to a new step
        /* if (preciseTimer >= stepDuration * stepCounter)
         {
             stepCounter++;
             Debug.Log(stepCounter);
             onStepUpdate();
         }*/
        //print((int)Math.Floor(preciseTimer * stepsPerSecond));
        ////print(Time.deltaTime);
        //if((int)Math.Floor(preciseTimer*stepsPerSecond) >= stepCounter)
        //{
        //    stepCounter++;
        //    Debug.Log(stepCounter);
        //    onStepUpdate();
        //}

        if (timeLeftInStep <= 0)
        {
            timeLeftInStep = stepDuration;
            stepCounter++;
            //Debug.Log(stepCounter);
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
            //print("current step attack list count: " + currentStepAttackList.Count);
            for (int i = 0; i < currentStepAttackList.Count; i++)
            {
                //print("i: " + i);
                currentStepAttackList[i].create();
            }
        }

        if (stepCounter >= 520)
        {
            SceneManager.LoadScene(sceneName: "CutsceneEnd");
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
            float yPos = 5.65f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 0));
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
            float xPos = 14.3f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 270));
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
