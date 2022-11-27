using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GenerateSteps : MonoBehaviour
{
    // Start is called before the first frame update
    public Object[] platforms;
    private Vector3 size;
    private Vector3 highest;
    public GameObject monsterTemplate1;
    public GameObject monsterTemplate2;
    public GameObject monsterTemplate3;
    public GameObject monsterTemplate4;
    public GameObject monsterTemplate5;
    private float xMin;
    public float xMax;
    [Header("Platform generation Parameters")]
    [Tooltip("Minimum y-distance for next platform")]
    public float yPlusMin = 5.0f;
    [Tooltip("Maximum y-distance for next platform")]
    public float yPlusMax = 15.0f;
    [Tooltip("Maximum x-distance for next platform")]
    public float xRadius = 5.0f;

    [Header("Spawn Chances (0-100%) (These cannot stack)")]
    public int staticChance = 0;
    public int zoomerChance = 0;
    public int scrollerChance = 0;
    public (float, float) scrollerSpeeds = (2,10);

    [Header("Addon Chances (0-100%) (these can stack)")]
    public int smallPlatformChance = 0;
    public int breakableChance = 0;
    public int superChance = 0;

    void Start()
    {
    }
    public void Generate(Vector3 lasthighest)
    {
        //Debug.Log(lasthighest);
        highest = lasthighest;
        size = GetComponent<Collider>().bounds.size;
        int maxPlatforms = 10;
        for (int i = 0; i < maxPlatforms; i++)
        {
            xMin = gameObject.transform.position.x - (size.x);
            xMax = gameObject.transform.position.x + (size.x);
            float xMinRan = Mathf.Max(xMin, highest.x - xRadius);
            float xMaxRan = Mathf.Min(xMax, highest.x + xRadius);
            //Debug.Log(xMin);
            //Debug.Log(xMax);

            float xPos = Random.Range(xMinRan, xMaxRan);
            float yDev = Random.Range(yPlusMin, yPlusMax);
            Vector3 newPos = highest + (Vector3.up * yDev);
            newPos.x = xPos;

            GameObject player = GameObject.Find("Player");
            //Transform playerTransform = player.transform;
            if(yDev > 10.4f){
                Scene scene = SceneManager.GetActiveScene();
                if(scene.name == "Level1-underground"){
                    MonsterGenerate(monsterTemplate1, newPos, scene);
                }
                if (scene.name == "Level2-underwater")
                {
                    //Debug.Log("111111111");
                    // GameObject monster2 = Instantiate<GameObject>(monsterTemplate2);
                    // monster2.transform.parent = gameObject.transform;
                    // monster2.transform.position = new Vector3((newPos.x + highest.x)/2, (newPos.y + highest.y)/2, 4.0f);
                    MonsterGenerate(monsterTemplate2, newPos, scene);
                }
                if(scene.name == "Level3-mountain"){
                    MonsterGenerate(monsterTemplate3, newPos, scene);
                }
                if(scene.name == "Level4-sky"){
                    MonsterGenerate(monsterTemplate4, newPos, scene);
                }
                if(scene.name == "Level5-space"){
                    MonsterGenerate(monsterTemplate5, newPos, scene);
                }
            }
            if (within(newPos))
            {
                spawnFab(newPos);
            }
            else
            {
                break;
            }
        }
    }

    private void MonsterGenerate(GameObject monsterTemplate, Vector3 newStepPos, Scene scene){
        GameObject monster = Instantiate<GameObject>(monsterTemplate);
        monster.transform.parent = gameObject.transform;
        if(scene.name == "Level1-underground" || scene.name == "Level2-underwater"){
            monster.transform.position = new Vector3((newStepPos.x + highest.x)/2, (newStepPos.y + highest.y)/2, 4.0f);
        }else{
            if(newStepPos.x > highest.x){
                monster.transform.position = new Vector3((newStepPos.x + highest.x)/2 + Random.Range(-25f, -12f), (newStepPos.y + highest.y)/2, 4.0f);
            }else{
                monster.transform.position = new Vector3((newStepPos.x + highest.x)/2 + Random.Range(12f, 25f), (newStepPos.y + highest.y)/2, 4.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // //get player position
        // GameObject player = GameObject.Find("Player");
        // Transform playerTransform = player.transform;


        // //iterate through steps and animate by poping out when player is higher than the step
        // for (int i = 0; i < steps.Count; i++)
        // {
        //     // - 1.0f so that player won't collide with the step
        //     if (playerTransform.position.y -1.0f> steps[i].transform.position.y)
        //     {
        //         //steps[i].transform.position = new Vector3(steps[i].transform.position.x, steps[i].transform.position.y, 4.0f);
        //         iTween.MoveTo(steps[i], new Vector3(steps[i].transform.position.x, steps[i].transform.position.y, 4.0f), 0.5f);
        //         steps.Remove(steps[i]);
        //     }
        // }

        // for (int i = 0; i < zoomers.Count; i++)
        // {
        //     // - 1.5f so that player won't collide with the step
        //     if (playerTransform.position.y - 0.5f > zoomers[i].Item1.transform.position.y)
        //     {
        //         //zoomers[i].transform.position = new Vector3(zoomers[i].transform.position.x, zoomers[i].transform.position.y, 4.0f);
        //         iTween.MoveTo(zoomers[i].Item1, new Vector3(zoomers[i].Item2.x, zoomers[i].Item2.y, 4.0f), 1.0f);
        //         zoomers.Remove(zoomers[i]);
        //     }
        // }

        // for (int i = 0; i < scrollers.Count; i++)
        // {
        //     if (scrollers[i].Item1.transform.position.x < xMin)
        //     {
        //         scrollers[i] = (scrollers[i].Item1, scrollers[i].Item2, 1);
        //     }
        //     else if (scrollers[i].Item1.transform.position.x > xMax)
        //     {
        //         scrollers[i] = (scrollers[i].Item1, scrollers[i].Item2, -1);
        //     }
        //     scrollers[i].Item1.transform.position += Time.deltaTime * Vector3.right * scrollers[i].Item2 * scrollers[i].Item3;
        // }
    }

    int sumList(int[] list)
    {
        int sum = 0;
        for (int i = 0; i < list.Length; i++)
        {
            sum += list[i];
        }
        return (sum);
    }

    private void spawnFab(Vector3 newPos)
    {
        GameObject gobj;


        //special modifiers
        bool breakable = false;
        bool superBounce = false;
        bool smallPlatform = false;

        //breakable platform?
        if (boolPercent(breakableChance))
        {
            breakable = true;
        }

        //super bouncy?
        if (boolPercent(superChance))
        {
            superBounce = true;
        }

        //small platform?
        if (boolPercent(smallPlatformChance))
        {
            smallPlatform = true;
        }




        //chance of spawning 'static' blocks
        if (boolPercent(staticChance))
        {
            if (!smallPlatform)
            {
                gobj = spawnStaticStep(newPos, 0);
            }
            else
            {
                gobj = spawnStaticStep(newPos, 1);
            }
        }

        //18% chance of spawning 'zoom' blocks
        else if (boolPercent(zoomerChance))
        {
            if (!smallPlatform)
            {
                gobj = spawnZoomStep(newPos, 0);
            }
            else
            {
                gobj = spawnZoomStep(newPos, 1);
            }
        }

        //chance of spawning scrolling steps
        else if (boolPercent(scrollerChance))
        {
            if (!smallPlatform)
            {
                gobj = spawnScrollStep(newPos, 0, Random.Range(scrollerSpeeds.Item1, scrollerSpeeds.Item2), (boolPercent(50) ? -1 : 1));
            }
            else
            {
                gobj = spawnScrollStep(newPos, 1, Random.Range(scrollerSpeeds.Item1, scrollerSpeeds.Item2), (boolPercent(50) ? -1 : 1));
            }
        }

        //otherwise, regular blocks that pop up
        else
        {
            if (!smallPlatform)
            {
                gobj = spawnPoppingStep(newPos, 0);
            }
            else
            {
                gobj = spawnPoppingStep(newPos, 1);
            }

        }



        //setting modifiers.
        if (breakable)
        {
            gobj.GetComponent<platformScript>().setBreakable();
        }
        if (superBounce)
        {
            gobj.GetComponent<platformScript>().increaseBounceTo();
        }
    }




    private GameObject spawnPoppingStep(Vector3 newPos, int type)
    {
        GameObject gobj = Instantiate(platforms[type]) as GameObject;

        //set the position.z value to 5.7 for the effect of step animation later
        gobj.transform.position = new Vector3(newPos.x, newPos.y, 5.7f);
        gobj.transform.parent = gameObject.transform;

        gobj.GetComponent<platformScript>().setPopout();

        highest = newPos;

        return gobj;
    }

    private GameObject spawnStaticStep(Vector3 newPos, int type)
    {
        GameObject gobj = Instantiate(platforms[type]) as GameObject;

        //set the position.z value to 5.7 for the effect of step animation later
        gobj.transform.position = new Vector3(newPos.x, newPos.y, 4.0f); ;

        gobj.transform.parent = gameObject.transform;
        highest = newPos;


        return gobj;
    }

    private GameObject spawnZoomStep(Vector3 newPos, int type)
    {
        GameObject gobj = Instantiate(platforms[type]) as GameObject;

        float spawnX = 30.0f;

        if (newPos.x < 0)
        {
            spawnX *= -1;
        }

        //set the position.z value to 5.7 for the effect of step animation later
        gobj.transform.position = new Vector3(spawnX, newPos.y, 4.0f);

        gobj.transform.parent = gameObject.transform;

        gobj.GetComponent<platformScript>().setZoomer(newPos);

        highest = newPos;



        return gobj;
    }

    private GameObject spawnScrollStep(Vector3 newPos, int type, float speed, int dir)
    {
        GameObject gobj = Instantiate(platforms[type]) as GameObject;

        gobj.transform.position = new Vector3(newPos.x, newPos.y, 4.0f);

        gobj.transform.parent = gameObject.transform;

        gobj.GetComponent<platformScript>().setScroller(speed, dir, new Vector2(xMin, xMax));

        highest = newPos;


        return gobj;
    }

    private bool boolPercent(int chance)
    {
        float rand = Random.Range(0, 100);

        if (rand < chance)
        {
            return true;
        }
        return false;
    }

    private bool within(Vector3 pos)
    {
        Vector3 backPos = gameObject.transform.position;
        float yMax = backPos.y + (size.y / 2);

        if (pos.y <= yMax)
        {
            return true;
        }
        return false;
    }

    public void setGenerationParameters(float yPlusMin, float yPlusMax, float xRadius)
    {
        this.yPlusMin = yPlusMin;
        this.yPlusMax = yPlusMax;
        this.xRadius = xRadius;

    }

    public void setUnstackableChances(int staticChance, int zoomerChance, int scrollerChance)
    {
        this.staticChance = staticChance;
        this.zoomerChance = zoomerChance;
        this.scrollerChance = scrollerChance;
    }

    public void setStackableChances(int smallPlatformChance, int breakableChance, int superChance)
    {
        this.smallPlatformChance = smallPlatformChance;
        this.breakableChance = breakableChance;
        this.superChance = superChance;
    }

    public void setAll(float a, float b, float c, int d, int e, int f, int g, int h, int i)
    {
        setGenerationParameters(a, b, c);
        setUnstackableChances(d, e, f);
        setStackableChances(g, h, i);
    }

    public void setScrollerSpeeds(float min, float max) {
        scrollerSpeeds = (min, max);
    }

    public void setScrollerSpeeds((float, float) details) {
        scrollerSpeeds = details;
    }

    // public List<GameObject> GetStepsPosition(){
    //     return steps;
    // }

}
