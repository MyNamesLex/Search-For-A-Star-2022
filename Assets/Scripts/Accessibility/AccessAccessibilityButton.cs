using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessAccessibilityButton : MonoBehaviour
{
    public GameObject AccessibilityPanel;

    public void OnClick()
    {
        if (AccessibilityPanel.activeInHierarchy == true)
        {
            AccessibilityPanel.SetActive(false);
        }
        else
        {
            AccessibilityPanel.SetActive(true);
        }
    }
}
