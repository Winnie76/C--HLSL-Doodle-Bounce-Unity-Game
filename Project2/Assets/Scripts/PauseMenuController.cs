using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenu;

    public Text ProgressText;
    public Text TimeValue;
    private float timePassed;
    private float totalHeight = 500.0f;
    private int timeMinute;
    private int timeSecond;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        timePassed = PlayerPrefs.GetFloat("timePassed");
        timeMinute = (int)(timePassed / 60.0f);
        timeSecond = (int)(timePassed - timeMinute * 60.0f);
        TimeValue.text = "Time: " + timeMinute + ":" + timeSecond;

        ProgressText.text = "Complete: " + (int)((PlayerPrefs.GetFloat("heightScore") / totalHeight) * 100) + "%";
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Levels");
    }
}
