using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGM : MonoBehaviour
{
    public static BGM instance = null;
    public float volume;
    public AudioSource audioSource;
    public AudioClip MainMenuBGM;
    public AudioClip IntroBGM;
    public AudioClip MainGameBGM;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        MainMenuLoad();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    public void MainMenuLoad()
    {
        audioSource.clip = MainMenuBGM;
        audioSource.Play();
    }
    public void MainGameLoad()
    {
        audioSource.clip = MainGameBGM;
        audioSource.Play();
    }
    public void IntroLoad()
    {
        audioSource.clip = IntroBGM;
        audioSource.Play();
    }

    public void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Main" && audioSource.clip != MainGameBGM)
        {
            MainGameLoad();
        }
        if (SceneManager.GetActiveScene().name == "MainMenu" && audioSource.clip != MainMenuBGM)
        {
            MainGameLoad();
        }
    }
}
