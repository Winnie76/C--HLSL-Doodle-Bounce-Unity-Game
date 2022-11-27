using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //get player position and compare with platform
        GameObject player = GameObject.Find("Player");
        Transform playerTransform = player.transform;
        if (playerTransform.position.y + 4.0f > this.gameObject.transform.position.y)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -0.5f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //get player position and compare with platform
        GameObject player = GameObject.Find("Player");
        Transform playerTransform = player.transform;
        if (playerTransform.position.y + 4.0f > this.gameObject.transform.position.y)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -0.5f);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>().velocity.y <= 0)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 300f);
        }
    }
}
