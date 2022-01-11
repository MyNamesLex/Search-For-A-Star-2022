using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessibility : MonoBehaviour
{
    public static Accessibility instance = null;
    [Header("Button Background Image Colour")]
    public bool BackgroundColourChanged = false;
    public Color BackgroundImagecolor;
    [Header("Button Text Colour")]
    public bool TextColourChanged = false;
    public Color Textcolor;
    [Header("Camera Shake")]
    public bool CameraShakeEnabled = true;
    [Header("Toggle Sprint")]
    public bool ToggleSprintEnabled = false;
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
}
