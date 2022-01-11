using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public EnvironmentFloater ef;
    public float HopIncreaseValue = 1;
    public float HopDecreaseValue = 1;
    public float Timer = 60;
    public bool Risen = false;
    public float RiseFallTimer;
    private float OGTimer;
    private bool InFunction = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!ef.UnderGlass)
        {
            gameObject.transform.localScale = new Vector3(3,3,3);
            OGTimer = Timer;
        }
    }

    public void Update()
    {
        if (!ef.UnderGlass)
        {
            if (Timer >= 0)
            {
                Timer -= Time.deltaTime;
            }

            if (transform.position.y <= 4.0f && Timer <= 0 && Risen == false && InFunction == false)
            {
                InFunction = true;
                StartCoroutine(HopIncreaseTimed());
            }
            if (transform.position.y >= 4.0f && Risen == true && InFunction == false)
            {
                InFunction = true;
                StartCoroutine(HopDecreaseTimed());
            }
        }
    }

    IEnumerator HopIncreaseTimed()
    {
        while (true)
        {
            yield return new WaitForSeconds(RiseFallTimer);
            transform.position = new Vector3(transform.position.x, transform.position.y + HopDecreaseValue, transform.position.z);
            if (transform.position.y >= 4)
            {
                Risen = true;
                InFunction = false;
                Timer = OGTimer;
                yield break;
            }
        }
    }

    IEnumerator HopDecreaseTimed()
    {
        while (true)
        {
            yield return new WaitForSeconds(RiseFallTimer);
            transform.position = new Vector3(transform.position.x, transform.position.y - HopDecreaseValue, transform.position.z);
            if (transform.position.y <= 0)
            {
                Risen = false;
                InFunction = false;
                yield break;
            }
        }
    }
}
