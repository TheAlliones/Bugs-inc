using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isOpened = false;
    private bool isMenue = false;
    public GameObject b0;
    public GameObject b1;
    public GameObject b2;
    public GameObject b3;
    public GameObject b4;

    public GameObject b5;
    public GameObject b6;

    void Start()
    {
        isOpened = false;
        b0.SetActive(false);
        b1.SetActive(false);
        b2.SetActive(false);
        b3.SetActive(false);
        b4.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isMenue)
            {
                if (isOpened)
                {
                    isOpened = false;
                    b0.SetActive(false);
                    b1.SetActive(false);
                    b2.SetActive(false);
                    b3.SetActive(false);
                    b4.SetActive(false);
                }
                else
                {
                    isOpened = true;
                    b0.SetActive(true);
                    b1.SetActive(true);
                    b2.SetActive(true);
                    b3.SetActive(true);
                    b4.SetActive(true);
                }
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpened)
            {
                isOpened = false;
                b0.SetActive(false);
                b1.SetActive(false);
                b2.SetActive(false);
                b3.SetActive(false);
                b4.SetActive(false);

            }
            else
            {
                if (isMenue)
                {
                    isMenue = false;
                    b5.SetActive(false);
                    b6.SetActive(false);
                }
                else
                {
                    isMenue = true;
                    b5.SetActive(true);
                    b6.SetActive(true);
                }
            }
            

        }
    }

    public void UIClicked()
    {
        isOpened = false;
        b0.SetActive(false);
        b1.SetActive(false);
        b2.SetActive(false);
        b3.SetActive(false);
        b4.SetActive(false);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMainMenue()
    {
        SceneManager.LoadScene(0);
    }
}
