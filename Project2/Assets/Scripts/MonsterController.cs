using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 0.5f;
    //private Rigidbody rb;
    private int movementDirection;
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level3-mountain" || scene.name == "Level4-sky" || scene.name == "Level5-space")
        {
            if (this.transform.position.y - player.transform.position.y < 15f)
            {
                //Debug.Log(this.transform.position.y);
                if (player.transform.position.x > this.transform.position.x)
                {
                    movementDirection = 1;
                }
                else
                {
                    movementDirection = -1;
                }

                this.transform.position = new Vector3(this.transform.position.x + movementDirection * moveSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == player)
        {

            GameOverCanvasController.GameOver = true;
        }
    }



}
