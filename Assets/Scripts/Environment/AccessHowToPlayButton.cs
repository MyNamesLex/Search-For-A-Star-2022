using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessHowToPlayButton : MonoBehaviour
{
    public GameObject HowToPlayPanel;

    public void OnClick()
    {
        if (HowToPlayPanel.activeInHierarchy == true)
        {
            HowToPlayPanel.SetActive(false);
        }
        else
        {
            HowToPlayPanel.SetActive(true);
        }
    }
}
