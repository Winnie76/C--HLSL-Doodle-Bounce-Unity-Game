using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteCanvasController : MonoBehaviour
{
    public GameObject gameCompletePanel;
    public static bool gameIsCompleted = false;
    //public static bool GameIsPaused = false;
    private float finishingHeight = 500.0f;
    private float heightValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get player position
        GameObject player = GameObject.Find("Player");
        Transform playerTransform = player.transform;
        heightValue = playerTransform.position.y;
        if (heightValue > finishingHeight)
        {
            gameCompletePanel.SetActive(true);
            Time.timeScale = 0f;
            gameIsCompleted = true;
        }
    }
    public void OnBackToMenuButtonPressed()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level1-underground")
        {
            GlobalOptions.currentLevel = 2;
        }
        if (sceneName == "Level2-underwater")
        {
            GlobalOptions.currentLevel = 3;
        }
        if (sceneName == "Level3-mountain")
        {
            GlobalOptions.currentLevel = 4;
        }
        if (sceneName == "Level4-sky")
        {
            GlobalOptions.currentLevel = 5;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
    }

    public void setFinishing(float height)
    {
        this.finishingHeight = height;
    }
}
