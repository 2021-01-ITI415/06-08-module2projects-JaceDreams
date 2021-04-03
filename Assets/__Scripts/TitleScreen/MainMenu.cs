using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayProspector ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayWestern()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlayBeach()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void PlayCasino()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }


    public void HomeButton (string __Main_Scene)
    {
        SceneManager.LoadScene(__Main_Scene);
    }

    public void QuitButton (string __Main_Scene)
    {
        SceneManager.LoadScene(__Main_Scene);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
}
