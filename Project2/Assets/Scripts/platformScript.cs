using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : MonoBehaviour
{   
    public bool willBreak = false;
    public float bounciness = 30.0f;

    public AudioClip sound_explode;
    public AudioClip sound_normalJump;
    public AudioClip sound_badJump;

    
    public GameObject player;
    private AudioSource audioSource;
    
    private MeshRenderer mesh;
    private float nexttime=0;
    private bool isGlowing = false;
    private bool isPlayerOver = false;


    //platform modifiers
    public bool isMoved = false;
    public bool isSuper = false;
    public bool isPopout = false;
    public bool isZoomer = false;
    public Vector3 zoomDestination;
    public bool isScroller = false;
    public int scrollDirection = -1;
    public float scrollSpeed;
    public Vector2 scrollBounds;
    
    
    void Start()
    {
        gameObject.tag = "Platform";
        audioSource = gameObject.GetComponent<AudioSource>();

        mesh = GetComponent<MeshRenderer>();
        ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
        //particleSystem.emission.rate = 0;

        player = GameObject.Find("Player");
        
        //default (static) colour is red
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        if (isZoomer) {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        if (isPopout) {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (isSuper) {
            foreach (Transform child in gameObject.transform) {
            if (child.gameObject.tag == "PlatformEffect") {
                child.gameObject.SetActive(true);
            }
        }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        isPlayerOver = player.transform.position.y - 1.5f > gameObject.transform.position.y;

        if (willBreak && Time.time >= nexttime) {
            if (isGlowing) {
                mesh.materials[0].EnableKeyword("_EMISSION");
                isGlowing = false;
            } else {
                mesh.materials[0].DisableKeyword("_EMISSION");
                isGlowing = true;
            }
            
            nexttime = Time.time + 0.5f;
        }


        //if platform is a popout type
        if (!isMoved && isPopout && isPlayerOver) {
            iTween.MoveTo(gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 4.0f), 0.5f);
            isPopout = false;
        }

        //if platform is zoomer type
        if (!isMoved && isZoomer && isPlayerOver) {
            iTween.MoveTo(gameObject, new Vector3(zoomDestination.x, zoomDestination.y, 4.0f), 1.0f);
            isZoomer = false;
        }

        //if platform is scrolling type
        if (isScroller) {
            //change direction if hits edge
            if (gameObject.transform.position.x < scrollBounds.x)
            {
                scrollDirection = 1;
            }

            //change direction if hits edge
            else if (gameObject.transform.position.x > scrollBounds.y)
            {
                scrollDirection = -1;
            }

            //moves the scrolling platform
            gameObject.transform.position += Time.deltaTime * Vector3.right * scrollSpeed * scrollDirection;
        }



        
    }

    void OnCollisionStay(Collision collision) {
        //breaking blocks
        //Debug.Log(collision.contacts[0].normal);

        //platform breaks when hit from the top
        if (willBreak && collision.contacts[0].normal == Vector3.down) {
            iTween.MoveBy(gameObject, Vector3.back * 50, 2.0f);
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("test");
        if (collision.contacts[0].normal == Vector3.down) {
            //audio
            if (isSuper) {
                audioSource.clip = sound_explode;
            } else {
                audioSource.clip = sound_normalJump;
            }

        } else {
            audioSource.clip = sound_badJump;
        }


        audioSource.Play();
    }

    public void setBreakable() {
        willBreak = true;
    }

    public void increaseBounceTo() {
        isSuper = true;
        bounciness *= 2;

        foreach (Transform child in gameObject.transform) {
            if (child.gameObject.tag == "PlatformEffect") {
                child.gameObject.SetActive(true);
            }
        }
    }

    public float getBounciness() {
        return bounciness;
    }

    public void setPopout() {
        //popout colour is yellow
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        isPopout = true;
    }

    public void setZoomer(Vector3 zoomDestination) {
        isZoomer = true;
        this.zoomDestination = zoomDestination;

        //zoomer colour is green
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void setScroller(float speed, int direction, Vector2 bounds) {

        isScroller = true;
        this.scrollSpeed = speed;
        this.scrollDirection = direction;
        this.scrollBounds = bounds;
    }
}
