using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadIntroTwo : MonoBehaviour
{
    public GameObject IntroObj;
    public GameObject IntroObjTwo;
    public void OnClick()
    {
        IntroObjTwo.SetActive(true);
        IntroObj.SetActive(false);
    }
}
