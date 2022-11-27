using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAndProgress : MonoBehaviour
{
    private float timePassed = 0.0f;
    private GameObject player;
    private float playerHeight = 0.0f;
    private float heightScore = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerHeight = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        PlayerPrefs.SetFloat("timePassed", timePassed);
        //Debug.Log(timePassed);
        playerHeight = player.transform.position.y;
        if(playerHeight >= heightScore){
            heightScore = playerHeight;
        }
        PlayerPrefs.SetFloat("heightScore", heightScore);
        // Debug.Log(playerHeight);
        // Debug.Log(heightScore + "---------");
    }

    // public float getHeight()
    // {
    //     return playerHeight;
    // }

    // public float getTime()
    // {
    //     Debug.Log(timePassed);
    //     return timePassed;
    // }
}
