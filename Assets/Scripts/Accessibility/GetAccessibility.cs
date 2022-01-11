using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GetAccessibility : MonoBehaviour
{
    public Accessibility access;
    public GameObject AccessibilityGameobject;
    [Header("Button Background Image Change")]
    public Color Imagecolor;
    public ButtonBackgroundAccessibility bba;
    [Header("Button Text Change")]
    public Color TextColour;
    public ButtonTextColourAccessibility btc;
    [Header("Camera Shake")]
    public bool CamShakeEnabled = true;
    [Header("Toggle Sprint")]
    public bool ToggleSprintEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if (AccessibilityGameobject == null)
        {
            AccessibilityGameobject = GameObject.FindGameObjectWithTag("AccessibilityTag");
            access = AccessibilityGameobject.GetComponent<Accessibility>();
        }
        if(bba == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("ImageBackgroundAccessibility");
            bba = g.GetComponent<ButtonBackgroundAccessibility>();
        }
        if (btc == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("ButtonTextChangeAccessibility");
            btc = g.GetComponent<ButtonTextColourAccessibility>();
        }
    }

    public void CamShakeEnable(bool value)
    {
        CamShakeEnabled = value;
        access.CameraShakeEnabled = CamShakeEnabled;
    }

    public void ButtonBackgroundImageColour(int value)
    {
        bba.ChangedList.Clear();
        access.BackgroundColourChanged = true;
        switch (value)
        {
            case 0: // default
                break;
            case 1: // green
                Imagecolor.r = 0;
                Imagecolor.g = 255;
                Imagecolor.b = 0;
                Imagecolor.a = 255;
                access.BackgroundImagecolor = Imagecolor;
                break;
            case 2: // blue
                Imagecolor.r = 0;
                Imagecolor.g = 0;
                Imagecolor.b = 255;
                Imagecolor.a = 255;
                access.BackgroundImagecolor = Imagecolor;
                break;
            case 3: // white
                Imagecolor.r = 255;
                Imagecolor.g = 255;
                Imagecolor.b = 255;
                Imagecolor.a = 255;
                access.BackgroundImagecolor = Imagecolor;
                break;
            case 4: // black
                Imagecolor.r = 0;
                Imagecolor.g = 0;
                Imagecolor.b = 0;
                Imagecolor.a = 255;
                access.BackgroundImagecolor = Imagecolor;
                break;
            case 5: // red
                Imagecolor.r = 255;
                Imagecolor.g = 0;
                Imagecolor.b = 0;
                Imagecolor.a = 255;
                access.BackgroundImagecolor = Imagecolor;
                break;
        }
    }
    public void ButtonTextColour(int value)
    {
        btc.ChangedList.Clear();
        access.TextColourChanged = true;
        switch (value)
        {
            case 0: // default
                break;
            case 1: // green
                TextColour.r = 0;
                TextColour.g = 255;
                TextColour.b = 0;
                TextColour.a = 255;
                access.Textcolor = TextColour;
                break;
            case 2: // blue
                TextColour.r = 0;
                TextColour.g = 0;
                TextColour.b = 255;
                TextColour.a = 255;
                access.Textcolor = TextColour;
                break;
            case 3: // white
                TextColour.r = 255;
                TextColour.g = 255;
                TextColour.b = 255;
                TextColour.a = 255;
                access.Textcolor = TextColour;
                break;
            case 4: // black
                TextColour.r = 0;
                TextColour.g = 0;
                TextColour.b = 0;
                TextColour.a = 255;
                access.Textcolor = TextColour;
                break;
            case 5: // red
                TextColour.r = 255;
                TextColour.g = 0;
                TextColour.b = 0;
                TextColour.a = 255;
                access.Textcolor = TextColour;
                break;
        }
    }

    public void ResetAccessibility()
    {
        foreach(TextMeshProUGUI t in btc.ChangedList)
        {
            if(SceneManager.GetActiveScene().name == "MainMenu")
            {
                t.color = Color.black;
            }
            if (SceneManager.GetActiveScene().name == "Main")
            {
                if (t.gameObject.tag == "DiceText")
                {
                    t.color = Color.black;
                }
                if (t.gameObject.tag == "SettingsText")
                {
                    t.color = Color.red;
                }
                if(t.gameObject.tag == "PauseMenuText")
                {
                    t.color = Color.red;
                }
                if (t.gameObject.tag == "MenuText")
                {
                    t.color = Color.black;
                }
                if (t.gameObject.tag == "ResultText")
                {
                    t.color = Color.white;
                }
                if(t.gameObject.tag == "AccessibilityText")
                {
                    t.color = Color.yellow;
                }
                if (t.gameObject.tag == "DropDownAndBackText")
                {
                    t.color = Color.black;
                }
            }
        }
        foreach(Image g in bba.ChangedList)
        {
            if(SceneManager.GetActiveScene().name == "MainMenu")
            {
                g.color = Color.white;
            }
            if (SceneManager.GetActiveScene().name == "Main")
            {
                g.color = Color.white;
            }
        }
        Imagecolor.r = 0;
        Imagecolor.g = 0;
        Imagecolor.b = 0;

        TextColour.g = 0;
        TextColour.r = 0;
        TextColour.b = 0;

        Imagecolor.a = 0;
        TextColour.a = 0;

        access.Textcolor.r = 0;
        access.Textcolor.g = 0;
        access.Textcolor.b = 0;
        access.Textcolor.a = 0;

        access.BackgroundImagecolor.r = 0;
        access.BackgroundImagecolor.b = 0;
        access.BackgroundImagecolor.g = 0;
        access.BackgroundImagecolor.a = 0;

        access.BackgroundColourChanged = false;
        access.CameraShakeEnabled = true;
        access.TextColourChanged = false;

        access.ToggleSprintEnabled = false;
    }

    public void ButtonImageREDslider(float value)
    {
        bba.ChangedList.Clear();
        Imagecolor.r = value;
        Imagecolor.a = 255;
        access.BackgroundImagecolor = Imagecolor;
        access.BackgroundColourChanged = true;
    }
    public void ButtonImageBLUEslider(float value)
    {
        bba.ChangedList.Clear();
        Imagecolor.b = value;
        Imagecolor.a = 255;
        access.BackgroundImagecolor = Imagecolor;
        access.BackgroundColourChanged = true;
    }
    public void ButtonImageGREENslider(float value)
    {
        bba.ChangedList.Clear();
        Imagecolor.g = value;
        Imagecolor.a = 255;
        access.BackgroundImagecolor = Imagecolor;
        access.BackgroundColourChanged = true;
    }
    public void ButtonTextREDslider(float value)
    {
        btc.ChangedList.Clear();
        TextColour.r = value;
        TextColour.a = 255;
        access.Textcolor = TextColour;
        access.TextColourChanged = true;
    }
    public void ButtonTextBLUEslider(float value)
    {
        btc.ChangedList.Clear();
        TextColour.b = value;
        TextColour.a = 255;
        access.Textcolor = TextColour;
        access.TextColourChanged = true;
    }
    public void ButtonTextGREENslider(float value)
    {
        btc.ChangedList.Clear();
        TextColour.g = value;
        TextColour.a = 255;
        access.Textcolor = TextColour;
        access.TextColourChanged = true;
    }

    public void ToggleSprint(bool value)
    {
        ToggleSprintEnabled = value;
        access.ToggleSprintEnabled = ToggleSprintEnabled;
    }
}
