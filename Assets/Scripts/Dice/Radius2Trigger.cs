using System.Collections;
using UnityEngine;

public class Radius2Trigger : MonoBehaviour
{
    public Dice dice;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BoardPiece")
        {
            if (other.GetComponent<Box>().IsDangerous == true)
            {
                other.GetComponent<Box>().IsDangerous = false;
                other.GetComponent<ParticleSystem>().Play();
                other.GetComponent<Box>()._textDisplay.gameObject.SetActive(true);
                other.GetComponent<Box>()._textDisplay.text = "Mine Clear";
                Debug.Log("was dangerous on 2" + other.gameObject.name);
            }
        }
    }
}
