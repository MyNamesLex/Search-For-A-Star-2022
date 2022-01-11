using MilkShake;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    public float currentX;
    public float currentY;
    public float sensitivity;
    private float YMin = -50f;
    private float YMax = 50f;
    public Quaternion rotation;
    public bool isSecondCam = false;

    [Header("Object To Look At")]
    public float distance;
    public Transform lookat;

    [Header("Cam Shake")]
    public Shaker shaker;
    public ShakePreset shaketype;

    public void LateUpdate()
    {
        if (isSecondCam == false)
        {
            currentX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            currentY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        }
        else
        {
            currentX += Input.GetAxisRaw("P2CamX") * sensitivity * Time.deltaTime;
            currentY += Input.GetAxisRaw("P2CamY") * sensitivity * Time.deltaTime;
        }
            currentY = Mathf.Clamp(currentY, YMin, YMax);

            Vector3 Direction = new Vector3(0, 0, -distance);

            rotation = Quaternion.Euler(-currentY, currentX, 0);

            transform.position = lookat.position + rotation * Direction;
            transform.LookAt(lookat.position);
    }

    public void SensitivitySlider(float value)
    {
        sensitivity = value;
    }

    public void ShakeCam()
    {
        shaker.Shake(shaketype);
    }
}
