using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BackdropGenerator : MonoBehaviour

{
    public GameObject cam;
    public UnityEngine.Object top;
    public UnityEngine.Object middles;
    public UnityEngine.Object bottom;
    public float prefabHeight = 20;

    [Range(0, 1)]
    public float whenToDelete = 0.75f;

    [Range(2, 100)]
    public int initFabsToRender = 10;
    public int toGenerate = 30;
    private int generated = 1;
    private bool topGenerated = false;
    private float finishHeight;

    private GameObject water;

    private LinkedList<GameObject> walls;

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
    public float scrollerSpeedMin = 90;
    public float scrollerSpeedMax = 300;

    [Header("Addon Chances (0-100%) (these can stack)")]
    public int smallPlatformChance = 0;
    public int breakableChance = 0;
    public int superChance = 0;



    // Start is called before the first frame update
    void Start()
    {

        //sending total height information to canvas.
        finishHeight = (toGenerate * 20.0f) - 19;
        GameObject canvas = GameObject.Find("Canvas");
        GameObject gameOverCanvas = GameObject.Find("GameOverCanvas");
        GameObject gameCompleteCanvas = GameObject.Find("GameCompleteCanvas");
        GameObject camera = GameObject.Find("Main Camera");
        try
        {
            canvas.GetComponent<HUDController>().setTotalHeight(finishHeight);
            gameOverCanvas.GetComponent<GameOverCanvasController>().setTotalHeight(finishHeight);
            gameCompleteCanvas.GetComponent<GameCompleteCanvasController>().setFinishing(finishHeight);
            camera.GetComponent<CameraScript>().setMaxHeight(finishHeight);
        }
        catch (Exception)
        {
        }



        water = GameObject.Find("Water");

        //spawn starting wall
        walls = new LinkedList<GameObject>();
        GameObject gobj = Instantiate(bottom) as GameObject;
        gobj.transform.position = gameObject.transform.position + ((prefabHeight / 2) * Vector3.up);
        gobj.transform.parent = gameObject.transform;

        walls.AddLast(gobj);
        initFabs();
    }

    // Update is called once per frame
    void Update()
    {
        if ((cam.transform.position.y > (walls.First.Value.transform.position.y + walls.Last.Value.transform.position.y) / 2))
        {
            if (generated <= toGenerate - 2)
            {
                cycle();
            }
            else if (!topGenerated)
            {
                spawnTop();
            }
        }
    }

    private void initFabs()
    {
        for (int i = 0; i < initFabsToRender - 2; i++)
        {
            spawnMiddles();
        }
    }

    private void cycle()
    {
        spawnMiddles();
        removeBot();

    }

    private void spawnFab(GameObject gobj)
    {
        Vector3 lastHighest = getHighestPlatformPos(walls.Last.Value);

        GameObject last = walls.Last.Value;
        Transform anchorTransform = last.transform.Find("top");
        UnityEngine.Assertions.Assert.IsNotNull(anchorTransform);

        gobj.transform.position = anchorTransform.position + ((prefabHeight / 2) * Vector3.up);

        try
        {
            gobj.GetComponent<GenerateSteps>().Generate(lastHighest);
        }
        catch (NullReferenceException)
        {
        }


        gobj.transform.parent = gameObject.transform;


        walls.AddLast(gobj);
        generated++;
    }

    private void spawnMiddles()
    {
        GameObject gobj = Instantiate(middles) as GameObject;
        gobj.GetComponent<GenerateSteps>().setAll(yPlusMin, yPlusMax, xRadius, staticChance, zoomerChance, scrollerChance, smallPlatformChance, breakableChance, superChance);
        gobj.GetComponent<GenerateSteps>().setScrollerSpeeds(scrollerSpeedMin, scrollerSpeedMax);
        spawnFab(gobj);
    }

    private void spawnTop()
    {
        GameObject gobj = Instantiate(top) as GameObject;
        spawnFab(gobj);

        topGenerated = true;

        if (water != null)
        {
            water.GetComponent<ChaserScript>().setMaxHeight(gobj.transform.position.y - 10.0f);
        }
    }

    private void removeBot()
    {
        Destroy(walls.First.Value);
        walls.RemoveFirst();
    }

    private Vector3 getHighestPlatformPos(GameObject obj)
    {
        Vector3 highest = new Vector3(0, -1, 0);
        foreach (Transform child in obj.transform)
        {
            if (child.name != "top" && child.name != "StoneMonster(Clone)")
            {
                if (child.position.y > highest.y)
                {
                    highest = child.position;
                }
            }
        }
        return highest;
    }


    public void updateGenerationParameters(float yPlusMin, float yPlusMax, float xRadius)
    {
        this.yPlusMin = yPlusMin;
        this.yPlusMax = yPlusMax;
        this.xRadius = xRadius;

    }

    public void updateUnstackableChances(int staticChance, int zoomerChance, int scrollerChance)
    {
        this.staticChance = staticChance;
        this.zoomerChance = zoomerChance;
        this.scrollerChance = scrollerChance;
    }

    public void updateStackableChances(int smallPlatformChance, int breakableChance, int superChance)
    {
        this.smallPlatformChance = smallPlatformChance;
        this.breakableChance = breakableChance;
        this.superChance = superChance;
    }

    public void updateAll(float a, float b, float c, int d, int e, int f, int g, int h, int i)
    {
        updateGenerationParameters(a, b, c);
        updateUnstackableChances(d, e, f);
        updateStackableChances(g, h, i);
    }

    public void updateScrollerSpeeds(float min, float max)
    {
        scrollerSpeedMin = min;
        scrollerSpeedMax = max;
    }
}
