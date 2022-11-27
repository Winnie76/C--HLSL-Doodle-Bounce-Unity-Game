using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverCanvasController : MonoBehaviour
{
    public static bool GameOver = false;
    public GameObject GameOverPanel;
    public Text ProgressText;
    public Text TimeValue;
    //public Slider progressBar;
    //private float heightScore;
    // private int progress;
    private float timePassed;
    private float totalHeight = 500.0f;
    private int timeMinute;
    private int timeSecond;

    // Update is called once per frame
    void Update()
    {
        timePassed = PlayerPrefs.GetFloat("timePassed");
        timeMinute = (int)(timePassed / 60.0f);
        timeSecond = (int)(timePassed - timeMinute * 60.0f);
        TimeValue.text = "Time: " + timeMinute + ":" + timeSecond;

        ProgressText.text = "Complete: " + (int)((PlayerPrefs.GetFloat("heightScore") / totalHeight) * 100) + "%";
        //Debug.Log(PlayerPrefs.GetFloat("heightScore") + "---------");
        if(GameOver){
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }


    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        GameOverCanvasController.GameOver = false;
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Levels");
    }

    public void setTotalHeight(float totalHeight) {
        this.totalHeight = totalHeight;
    }
}