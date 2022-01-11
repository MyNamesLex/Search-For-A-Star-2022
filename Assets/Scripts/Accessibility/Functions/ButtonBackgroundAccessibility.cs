using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBackgroundAccessibility : MonoBehaviour
{
    public Accessibility access;
    public Image[] ButtonBackgroundChange;
    public List<Image> ChangedList;

    // Update is called once per frame
    void Update()
    {
        if(access == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("AccessibilityTag");
            access = g.gameObject.GetComponent<Accessibility>();
        }
        if (access != null)
        {
            //Button Background Colour
            if (access.BackgroundColourChanged == false)
            {
                //Destroy(gameObject);
            }
            else if (ChangedList.Count != ButtonBackgroundChange.Length && access != null)
            {
                foreach (Image i in ButtonBackgroundChange)
                {
                    if (i != null && !ChangedList.Contains(i))
                    {
                        i.color = access.BackgroundImagecolor;
                        ChangedList.Add(i);
                    }
                }
            }
        }
    }
}
