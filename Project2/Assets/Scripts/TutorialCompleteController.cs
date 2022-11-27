using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCompleteController : MonoBehaviour
{
    public GameObject gameCompletePanel;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    //public static bool GameIsPaused = false;
    private int finishingHeight = 147;
    private int heightValue;
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
        heightValue = (int)playerTransform.position.y;
        if (heightValue > finishingHeight && monster1 == null && monster2 == null && monster3 == null)
        {
            gameCompletePanel.SetActive(true);
            Time.timeScale = 0f;
            //GameIsPaused = true;
        }
    }
    public void OnBackToMenuButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
    }
}

