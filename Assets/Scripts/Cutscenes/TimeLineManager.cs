using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour
{
    [Header("Shark Lose")]
    public GameObject SharkLoseClip;
    public GameObject SharkLoseCam;

    public void StartSharkCutscene()
    {
        SharkLoseCam.SetActive(true);
        SharkLoseClip.SetActive(true);
    }
}
