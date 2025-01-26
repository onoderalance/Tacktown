using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BossManagerScript : MonoBehaviour
{
    public GameObject projectile;
    public GameObject homingMissile;
    public GameObject rotatingSpike;

    float preciseTimer = 0.0f; //delta time
    int stepCounter = 0; //increases every (stepTime) seconds
    bool stepUpdated = false;
    public float stepsPerSecond = 1.0f;

    enum AttackType    {SHOT_FROM_TOP, SHOT_FROM_BOTTOM, SHOT_FROM_LEFT, SHOT_FROM_RIGHT,
                    BURST_FROM_TOP, BURST_FROM_BOTTOM, BURST_FROM_LEFT, BURST_FROM_RIGHT,
                    SHOT_FROM_CENTER, MISSILE_FROM_CENTER};

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
            //print("CREATEEE");
            float yPos = 1.0f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 0));
            newProjectile.GetComponent<TackProjectileScript>().speed = speed;
        }

    }

    public class SingleShotFromBottom:Attack
    {
        private float xPos;
        private float speed;

        public SingleShotFromBottom(float xPos, float speed)
        {
            this.xPos = xPos;
            this.speed = speed;
        }

        public override void create()
        {
            float yPos = -1.78f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            //GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 180));
            //newProjectile.GetComponent<TackProjectileScript>().speed = speed;
        }
    }

    public class SingleShotFromLeft:Attack
    {
        private float yPos;
        private float speed;

        public SingleShotFromLeft(float yPos, float speed)
        {
            this.yPos = yPos;
            this.speed = speed;
        }

        public override void create()
        {
            float xPos = -6.27f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            //GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 90));
            //newProjectile.GetComponent<TackProjectileScript>().speed = speed;
        }
    }

    public class SingleShotFromRight:Attack
    {
        private float yPos;
        private float speed;

        public SingleShotFromRight(float yPos, float speed)
        {
            this.yPos = yPos;
            this.speed = speed;
        }

        public override void create()
        {
            float xPos = -14.3f;
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
            //GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 270));
            //newProjectile.GetComponent<TackProjectileScript>().speed = speed;
        }
    }


    Dictionary<int, List<Attack>> attackList; //maps a time to a list of attacks that will happen on this step

    // Start is called before the first frame update
    void Start()
    {

        //projectile = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BossFightEnemies/BossProjecile.prefab", typeof(GameObject));
        //GameObject newProjectile = Instantiate(projectile, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));


        attackList = new Dictionary<int, List<Attack>> {
            [3] = new List<Attack> {(SingleShotFromTop)(new SingleShotFromTop(1.5f, 2.0f, projectile)) },
        };

    }

    // Update is called once per frame
    void Update()
    {
        preciseTimer += Time.deltaTime;

        //Debug.Log((int)(preciseTimer/stepsPerSecond));

        //call onStepUpdate if we have just moved to a new step
        if ((int)(preciseTimer / stepsPerSecond) != stepCounter)
        {
            stepCounter = (int)(preciseTimer/stepsPerSecond);
            Debug.Log(stepCounter);
            onStepUpdate();
        }

    }

    //only called when the step counter is updated
    void onStepUpdate() {

        //if there is an attack on this step:
        if (attackList.ContainsKey(stepCounter)) {
            //print("CONTAINS KEY");
            List<Attack> currentStepAttackList = attackList[stepCounter]; //a list of attacks happening on this step
            //print(currentStepAttackList);
            for (int i = 0; i < currentStepAttackList.Count; i++)
            {
                //print("i: " + i);
                currentStepAttackList[i].create();
            }
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

    void singleShotFromCenter(float angle)
    {
        //GameObject newProjectile = Instantiate(projectile, centerPosition, Quaternion.Euler(0, 0, angle));
    }

    void homingMissleFromCenter() {
        //GameObject newProjectile = Instantiate(homingMissile, centerPosition, Quaternion.Euler(0, 0, 0));
    }

}
