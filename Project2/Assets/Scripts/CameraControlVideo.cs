using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlVideo : MonoBehaviour
{
    public float cameraSpeed = 15.0f;
    private float camSens = 0.25f;

    public Vector3 mouseP = new Vector3();
    public Vector3 mouseStartP = new Vector3();
    public Vector3 mouseChangeP = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(0.41f, 0.0f, -31.177f);

        mouseStartP = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        mouseChangeP = Input.mousePosition - mouseStartP;
        mouseStartP =  Input.mousePosition;
        mouseChangeP = new Vector3(-mouseChangeP.y * camSens, mouseChangeP.x * camSens, 0 );
        mouseP = new Vector3(transform.eulerAngles.x + mouseChangeP.x , transform.eulerAngles.y + mouseChangeP.y, 0);
        transform.eulerAngles = mouseP;

        if (Input.GetKey ("w")) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + cameraSpeed * Time.deltaTime, this.transform.position.z);
        }
        if (Input.GetKey ("s")) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (-1) * cameraSpeed * Time.deltaTime, this.transform.position.z);
        }
        if (Input.GetKey ("d")) {
            this.transform.position = new Vector3(this.transform.position.x + cameraSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey ("a")) {
            this.transform.position = new Vector3(this.transform.position.x + (-1) * cameraSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey ("r")) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey ("f")) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (-1) * cameraSpeed * Time.deltaTime);
        }
    }
}
