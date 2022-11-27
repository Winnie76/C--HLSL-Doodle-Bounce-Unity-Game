using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialCanvas : MonoBehaviour
{
    public GameObject guide1;
    public GameObject guide2;
    public GameObject guide3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((int)HUDController.timePassed == 1)
        {
            guide1.SetActive(true);
        }
        if ((int)HUDController.timePassed == 6)
        {
            guide1.SetActive(false);
            guide2.SetActive(true);
        }
        if ((int)HUDController.timePassed == 11)
        {
            guide2.SetActive(false);
            guide3.SetActive(true);
        }
        if ((int)HUDController.timePassed == 16)
        {
            guide3.SetActive(false);
        }
    }
}
