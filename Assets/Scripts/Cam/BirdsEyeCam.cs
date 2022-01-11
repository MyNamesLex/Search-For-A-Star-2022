using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsEyeCam : MonoBehaviour
{
    public Camera birdseyecam;

    public void Start()
    {
        birdseyecam = GetComponent<Camera>();
    }
}
