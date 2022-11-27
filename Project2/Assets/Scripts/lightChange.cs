using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightChange : MonoBehaviour
{
    private float totalHeight = 500.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xRotate = 50 * (PlayerPrefs.GetFloat("heightScore") / totalHeight);
        this.transform.eulerAngles = new Vector3(xRotate, 0, 0);
    }
}
