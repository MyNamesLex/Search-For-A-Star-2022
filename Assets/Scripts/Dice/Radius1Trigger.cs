using System.Collections;
using UnityEngine;

public class Radius1Trigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BoardPiece")
        {
            if (other.GetComponent<Box>().IsDangerous == true)
            {
                other.GetComponent<ParticleSystem>().Play();
                Debug.Log("None Dangerous, landed on 5");
                other.GetComponent<Box>().OnClick(other.gameObject);
            }
            else
            {
                Debug.Log("Dangerous, landed on 5");
                other.GetComponent<Box>().OnClick(other.gameObject);
            }
        }
    }
}
