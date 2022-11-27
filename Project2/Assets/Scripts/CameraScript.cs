using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{

    public float followSpeed = 10.0f;
    // Start is called before the first frame update

    private AudioSource bgM;
    private float maxHeight = 10000.0f;
    private string sceneName;
    void Start()
    {
        //get player position
        GameObject player = GameObject.Find("Player");
        Transform playerTransform = player.transform;

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        //height of camera is the same as player
        this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);

        //bgm
        bgM = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        //get player position
        GameObject player = GameObject.Find("Player");
        Transform playerTransform = player.transform;

        if (sceneName == "Level5-space")
        {
            if (player.gameObject.GetComponent<Rigidbody>().velocity.y >= 0)
            {
                Vector3 positionToFollow = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
                this.transform.position = Vector3.Lerp(this.transform.position, positionToFollow, followSpeed * Time.deltaTime);
            }
        }
        else
        {
            //height of camera is the same as player
            Vector3 positionToFollow = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, positionToFollow, followSpeed * Time.deltaTime);
        }


        //bgm
        bgM.pitch = 0.75f + 0.4f * player.transform.position.y / maxHeight;
    }

    public void setMaxHeight(float height)
    {
        this.maxHeight = height;
    }

}