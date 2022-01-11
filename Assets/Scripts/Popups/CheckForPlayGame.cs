using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckForPlayGame : MonoBehaviour
{
    public CheckForPlayMain cfpm;
    public Popups pup;
    public bool Read = false;

    public void Update()
    {
        if (cfpm == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("FirstPlayCheck");
            cfpm = g.GetComponent<CheckForPlayMain>();
        }
        if (cfpm != null && Read == false)
        {
            Read = true;
            if (cfpm.HasPlayedSinglePlayer == true && SceneManager.GetActiveScene().name == "Main")
            {
                pup.Remove = true;
                gameObject.SetActive(false);
                return;
            }
            else if(SceneManager.GetActiveScene().name == "Main")
            {
                pup.ShowPopups = true;
                cfpm.HasPlayedSinglePlayer = true;
                gameObject.SetActive(false);
                return;
            }

            if (cfpm.HasPlayedTwoPlayer == true && SceneManager.GetActiveScene().name == "TwoPlayer")
            {
                pup.Remove = true;
                gameObject.SetActive(false);
                return;
            }

            else if (SceneManager.GetActiveScene().name == "TwoPlayer")
            {
                pup.ShowPopups = true;
                cfpm.HasPlayedTwoPlayer = true;
                gameObject.SetActive(false);
                return;
            }
        }
    }

}
