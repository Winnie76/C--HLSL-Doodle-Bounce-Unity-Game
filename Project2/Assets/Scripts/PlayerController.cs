using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float defaultSpeed = 20f;
    private Rigidbody rb;
    private int directionInput;
    public GameObject projectile;
    public AudioSource jumpAudio;

    public GameObject shiftSpeedTrail;

    //public GameObject gameOver;


    private bool leftMouseButtonPressed;
    private bool leftDirectionPressed;
    private bool rightDirectionPressed;
    private bool downDirectionPressed;
    private bool speedKeyPressed;
    private float fallMultiplier = 3.5f;
    private float riseMultiplier = 3f;
    private float speed;
    private TrailRenderer speedtrail;
    private string sceneName;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpAudio = GetComponent<AudioSource>();
        speedtrail = GetComponent<TrailRenderer>();
        speedtrail.enabled = false;
        shiftSpeedTrail.GetComponent<ParticleSystem>().Stop();

        
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            leftDirectionPressed = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rightDirectionPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            downDirectionPressed = true;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space))
        {
            speedKeyPressed = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            leftMouseButtonPressed = true;
        }
    }

    void FixedUpdate()
    {
        // Keep shiftspeedtrail off unless needed
        shiftSpeedTrail.GetComponent<ParticleSystem>().Stop();

        // Player Momentum Difficulty Scale
        if (rb.velocity.y < 0)
        {
            // speed up upwards momentum gradually with height increase
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            // speed up downwards momentum gradually with height increase
            rb.velocity += Vector3.up * Physics.gravity.y * (riseMultiplier - 1) * Time.deltaTime;
        }

        // Player Left and Right movement
        if (leftDirectionPressed)
        {
            directionInput = -1;
            leftDirectionPressed = false;
            // only want trail when shift and moving left/right
            if (speedKeyPressed)
            {
                shiftSpeedTrail.GetComponent<ParticleSystem>().Play();
            }
        }
        if (rightDirectionPressed)
        {
            directionInput = 1;
            rightDirectionPressed = false;
            // only want trail when shift and moving left/right
            if (speedKeyPressed)
            {
                shiftSpeedTrail.GetComponent<ParticleSystem>().Play();
            }
        }

        // Player Projectile Logic
        if (leftMouseButtonPressed)
        {
            // Make a new projectile object using prefab
            // Initialised to player position and 0 rotation
            GameObject p = Instantiate<GameObject>(projectile, this.transform.position, Quaternion.identity);
            leftMouseButtonPressed = false;
        }

        speed = defaultSpeed;
        if (speedKeyPressed)
        {
            speed = defaultSpeed * 2;
            speedKeyPressed = false;
        }

        rb.velocity = new Vector3(directionInput * speed, rb.velocity.y, 0.0f);
        if (downDirectionPressed && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(0, -8f, 0);
            downDirectionPressed = false;
        }

        if (sceneName == "Level5-space") {
            GameOver();
        }

        directionInput = 0;
    }

    void GameOver()
    {
        if(rb.velocity.y < 0 && this.transform.position.y < Camera.main.transform.position.y - 20f){
            GameOverCanvasController.GameOver = true;
            Debug.Log("Game Over");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player touches the water below insta die.
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            if (sceneName != "Level5-space") {
                GameOverCanvasController.GameOver = true;
            }  
        }

        if (collision.gameObject.tag == "Platform") {
            /*play jumpsound*/
                jumpAudio.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].normal.Equals(new Vector3(0f, 1f, 0f)))
        {
            //get bounciness of platform
            float bounciness = 30.0f;
            if (collision.gameObject.tag == "Platform")
            {
                bounciness = collision.gameObject.GetComponent<platformScript>().getBounciness();
            }

            //enable trail for bounce if speed is high.
            if (bounciness > 40.0f)
            {
                speedtrail.enabled = true;
            }
            else
            {
                speedtrail.enabled = false;
            }

            rb.velocity = new Vector3(0.0f, bounciness, 0.0f);

           



        }
    }
}
