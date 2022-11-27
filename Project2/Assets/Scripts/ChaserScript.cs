using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaserScript : MonoBehaviour
{
    public int trailDistance = 20;

    private float maxHeight = -1;
    public GameObject player;

    private Renderer rend;
    public float scrollSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y - trailDistance, this.transform.position.z);
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player is ascending
        if (player.gameObject.GetComponent<Rigidbody>().velocity.y > 0 && (maxHeight == -1 ? true : player.transform.position.y <= maxHeight)) {
            if (player.transform.position.y - this.transform.position.y > trailDistance) {
                this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y - trailDistance, this.transform.position.z);
            }
        }
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;


        if (sceneName == "Level3-mountain" || sceneName == "Level4-sky") {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
        }
    }

    public void setMaxHeight(float maxHeight) {
        this.maxHeight = maxHeight;
    }
}
