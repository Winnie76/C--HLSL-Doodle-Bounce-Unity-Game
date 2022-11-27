using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    //public Text HeightText;
    public Text TimeValue;
    public GameObject LavaWarning;
    public GameObject BreakableWarning;
    public GameObject ZoomWarning;
    public GameObject FallWarning;
    public Slider progressBar;
    public GameObject InGamePanel;
    private float heightValue;
    //private int heightPercentage;
    public static float timePassed;
    private float totalHeight = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0.0f;
        InGamePanel.SetActive(true);
        TimeValue.text = ((int)timePassed).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //get player position
        GameObject player = GameObject.Find("Player");
        Transform playerTransform = player.transform;
        heightValue = playerTransform.position.y;

        //time passed since the beginning of the game
        timePassed += Time.deltaTime;
        TimeValue.text = ((int)timePassed).ToString();
        //show height percentage of player
        progressBar.value = heightValue / totalHeight;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level1-underground")
        {
            if ((int)timePassed == 1)
            {
                LavaWarning.SetActive(true);
            }
            if ((int)timePassed >= 5)
            {
                LavaWarning.SetActive(false);
            }
        }
        else if (sceneName == "Level3-mountain")
        {
            if ((int)timePassed == 1)
            {
                BreakableWarning.SetActive(true);
            }
            if ((int)timePassed >= 5)
            {
                BreakableWarning.SetActive(false);
            }
        }
        else if (sceneName == "Level4-sky")
        {
            if ((int)timePassed == 1)
            {
                ZoomWarning.SetActive(true);
            }
            if ((int)timePassed >= 5)
            {
                ZoomWarning.SetActive(false);
            }
        }
        else if (sceneName == "Level5-space")
        {
            if ((int)timePassed == 1)
            {
                FallWarning.SetActive(true);
            }
            if ((int)timePassed >= 5)
            {
                FallWarning.SetActive(false);
            }
        }

        if (GameOverCanvasController.GameOver || PauseMenuController.GameIsPaused)
        {
            InGamePanel.SetActive(false);
        }

    }

    public void setTotalHeight(float totalHeight)
    {
        this.totalHeight = totalHeight;
    }
    public float getHeightPercentage()
    {
        return progressBar.value;

    }
}
