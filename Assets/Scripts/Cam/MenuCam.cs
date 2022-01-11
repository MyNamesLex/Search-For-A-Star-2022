using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    public float turndegree;
    public void Update()
    {
        transform.Rotate(Vector3.up, turndegree * Time.deltaTime);
    }
}
