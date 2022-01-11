using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius3Trigger : MonoBehaviour
{
    public Material OriginalMaterial;
    public Material minemat;
    public List<GameObject> Seen;
    public bool StopAdd = false;

    // this wont work the second time it is called?
    public void Awake()
    {
        StartCoroutine(Timeout());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoardPiece") && other.GetComponent<Box>().IsDangerous == true)
        {
            OriginalMaterial = other.GetComponent<MeshRenderer>().material;
            if (Seen.Contains(other.gameObject) == false && StopAdd == false)
            {
                Seen.Add(other.gameObject);
                Debug.Log("Was Dangerous on 3 " + other.gameObject.name);
                other.GetComponent<MeshRenderer>().material = minemat;
                other.GetComponent<ParticleSystem>().Play();
                StartCoroutine(Timeout());
            }
        }
    }

    IEnumerator Timeout()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            StopAdd = true;
            Stop();
            yield break;
        }
    }

    public void Stop()
    {
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            StopCoroutine(Timeout());
            Seen.Clear();
            StopAdd = false;
            gameObject.SetActive(false);
            yield break;
        }
    }
}
