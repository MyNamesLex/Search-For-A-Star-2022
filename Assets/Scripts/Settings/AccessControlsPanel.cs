using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessControlsPanel : MonoBehaviour
{
    public GameObject ControlsPanel;

    public void OnClick()
    {
        if (ControlsPanel.activeInHierarchy == true)
        {
            ControlsPanel.SetActive(false);
        }
        else
        {
            ControlsPanel.SetActive(true);
        }
    }
}
