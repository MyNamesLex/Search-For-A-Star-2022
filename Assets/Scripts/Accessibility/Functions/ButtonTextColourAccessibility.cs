using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonTextColourAccessibility : MonoBehaviour
{
    public Accessibility access;
    public TextMeshProUGUI[] ButtonTextChange;
    public List<TextMeshProUGUI> ChangedList;

    // Update is called once per frame
    void Update()
    {
        if (access == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("AccessibilityTag");
            access = g.gameObject.GetComponent<Accessibility>();
        }
        if (access != null)
        {
            //Button Background Colour
            if (access.TextColourChanged == false)
            {
                //Destroy(gameObject);
            }
            else if (ChangedList.Count != ButtonTextChange.Length && access != null)
            {
                foreach (TextMeshProUGUI t in ButtonTextChange)
                {
                    if (t != null && !ChangedList.Contains(t))
                    {
                        t.color = access.Textcolor;
                        ChangedList.Add(t);
                    }
                }
            }
        }
    }
}
