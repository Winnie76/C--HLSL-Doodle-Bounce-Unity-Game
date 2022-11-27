using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsController : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject star5;
    public GameObject diamond;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    public GameObject levelTut;
    private Vector3 starScale;
    private Vector3 newScale;
    // Start is called before the first frame update
    void Start()
    {
        starScale = this.transform.localScale;
        newScale = starScale + new Vector3(0.1f, 0.1f, 0.1f);
        if (GlobalOptions.currentLevel == 2)
        {
            star2.SetActive(true);

        }
        if (GlobalOptions.currentLevel == 3)
        {
            star2.SetActive(true);
            star3.SetActive(true);
        }
        if (GlobalOptions.currentLevel == 4)
        {
            star2.SetActive(true);
            star3.SetActive(true);
            star4.SetActive(true);
        }
        if (GlobalOptions.currentLevel == 5)
        {
            star2.SetActive(true);
            star3.SetActive(true);
            star4.SetActive(true);
            star5.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        diamond.transform.Rotate(Vector3.forward * 30 * Time.deltaTime, Space.Self);
    }
    void OnMouseOver()
    {
        if (this.transform.localScale.x < newScale.x)
        {
            this.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            if (this.gameObject.name == "SoftStar1")
            {
                level1.SetActive(true);
            }
            if (this.gameObject.name == "SoftStar2")
            {
                level2.SetActive(true);
            }
            if (this.gameObject.name == "SoftStar3")
            {
                level3.SetActive(true);
            }
            if (this.gameObject.name == "SoftStar4")
            {
                level4.SetActive(true);
            }
            if (this.gameObject.name == "SoftStar5")
            {
                level5.SetActive(true);
            }
            if (this.gameObject.name == "diamond")
            {
                levelTut.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(0) && this.gameObject.name == "diamond")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Tutorial");

        }
        if (Input.GetMouseButtonDown(0) && this.gameObject.name == "SoftStar1")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level1-underground");
        }
        if (Input.GetMouseButtonDown(0) && this.gameObject.name == "SoftStar2")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level2-underwater");

        }
        if (Input.GetMouseButtonDown(0) && this.gameObject.name == "SoftStar3")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level3-mountain");
        }
        if (Input.GetMouseButtonDown(0) && this.gameObject.name == "SoftStar4")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level4-sky");
        }
        if (Input.GetMouseButtonDown(0) && this.gameObject.name == "SoftStar5")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level5-space");
        }

    }

    void OnMouseExit()
    {
        if (this.transform.localScale.x > starScale.x)
        {
            this.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }
        if (this.gameObject.name == "SoftStar1")
        {
            level1.SetActive(false);
        }
        if (this.gameObject.name == "SoftStar2")
        {
            level2.SetActive(false);
        }
        if (this.gameObject.name == "SoftStar3")
        {
            level3.SetActive(false);
        }
        if (this.gameObject.name == "SoftStar4")
        {
            level4.SetActive(false);
        }
        if (this.gameObject.name == "diamond")
        {
            levelTut.SetActive(false);
        }
    }
}
