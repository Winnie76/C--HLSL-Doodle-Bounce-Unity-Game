using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPlatformFolding : MonoBehaviour
{
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

        // - 1.0f so that player won't collide with the step
        if (playerTransform.position.y - 1.0f > this.transform.position.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 4.0f);
        }
    }
}
