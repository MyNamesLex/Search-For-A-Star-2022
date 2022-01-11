using UnityEngine;

public class DiceCam : MonoBehaviour
{
    [Header("Movement")]
    public float XViewAngle;
    public float YViewAngle;
    public float sensitivity;
    public Quaternion viweangle;

    [Header("Object To Look At")]
    public float distance;
    public Transform lookat;

    public void LateUpdate()
    {
        Vector3 Direction = new Vector3(0, 0, -distance);

        viweangle = Quaternion.Euler(YViewAngle, XViewAngle, 0);

        transform.position = lookat.position + viweangle * Direction;
        transform.LookAt(lookat.position);
    }

    public void SensitivitySlider(float value)
    {
        sensitivity = value;
    }
}
