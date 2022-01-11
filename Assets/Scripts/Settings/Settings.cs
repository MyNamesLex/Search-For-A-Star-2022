using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public UniversalRenderPipelineAsset[] qs;
    public TMP_Dropdown qualitydropdown;

    public void Start()
    {
        qualitydropdown.value = QualitySettings.GetQualityLevel();
    }
    public void Quality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        QualitySettings.renderPipeline = qs[quality];
    }
    public void Fullscreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
    public void MasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }
    public void SFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", volume);
    }
    public void BGMVolume(float volume)
    {
        mixer.SetFloat("BGMVolume", volume);
    }
}
