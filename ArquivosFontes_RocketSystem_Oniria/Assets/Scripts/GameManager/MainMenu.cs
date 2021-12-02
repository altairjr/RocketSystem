using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons UI")]
    [SerializeField] private GameObject buttonLaunch_;
    [SerializeField] private GameObject buttonReturn_;

    public static bool startGame_;
    public static bool return_;

    private void Update()
    {
        if (startGame_)
        {
            buttonLaunch_.SetActive(false);
            buttonReturn_.SetActive(false);
        }

        if(!startGame_ && Rocket.launch > 0)
        {
            buttonLaunch_.SetActive(false);
            buttonReturn_.SetActive(true);
        }

        if (!startGame_ && Rocket.launch == 0)
            buttonReturn_.SetActive(false);
    }

    public void ButtonStart()
    {
        startGame_ = !startGame_;
    }

    public void ButtonReturn()
    {
        SceneManager.LoadScene("Rocket");
        startGame_ = false;
        Rocket.launch = 0;
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
