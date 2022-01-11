using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButtons : MonoBehaviour
{
    public GameObject RemoveMainMenuObj;
    public GameObject ActivateIntroObj;
    public CheckForPlayMain cfpm;
    public void LoadIntroObj()
    {
        if (cfpm.SeenIntro == false)
        {
            cfpm.SeenIntro = true;
            ActivateIntroObj.SetActive(true);
            RemoveMainMenuObj.SetActive(false);
        }
    }

    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu" && !cfpm)
        {
            GameObject g = GameObject.FindGameObjectWithTag("FirstPlayCheck");
            cfpm = g.GetComponent<CheckForPlayMain>();
        }
    }

    public void LoadMainScene()
    {
        if (cfpm.SeenIntro == false)
        {
            LoadIntroObj();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadAccessibility()
    {
        SceneManager.LoadScene(7);
    }
    public void LoadDiceEffects()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadControls()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadTwoPlayer()
    {
        if (cfpm.SeenIntro == false)
        {
            LoadIntroObj();
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
