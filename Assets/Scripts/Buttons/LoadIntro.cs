using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadIntro : MonoBehaviour
{
    public float TimeUntilNextMsg;
    public GameObject Msg1;
    public GameObject Msg2;
    public GameObject Msg3;
    public GameObject Msg4;
    public GameObject Msg5;
    public GameObject Msg6;
    public GameObject Msg7;
    public GameObject Msg8;
    public BGM bgm;
    public bool findbgm = false;

    public void Start()
    {
        if (gameObject.name == "IntroObj")
        {
            findbgm = true;
        }
        StartCoroutine(StaggerCourotine());
    }

    public void Update()
    {
        if(findbgm == true && bgm == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("BGM");
            bgm = g.GetComponent<BGM>();
            if(bgm!=null)
            {
                findbgm = false;
                bgm.IntroLoad();
            }
        }
    }

    IEnumerator StaggerCourotine()
    {
        while(true)
        {
            if (Msg1 != null)
            {
                yield return new WaitForSeconds(TimeUntilNextMsg);
                Msg1.SetActive(true);
            }
            if (Msg2 != null)
            {
                yield return new WaitForSeconds(TimeUntilNextMsg);
                Msg2.SetActive(true);
            }
            if (Msg3 != null)
            {
                yield return new WaitForSeconds(TimeUntilNextMsg);
                Msg3.SetActive(true);
            }
            if (Msg4 != null)
            {
                yield return new WaitForSeconds(TimeUntilNextMsg);
                Msg4.SetActive(true);
            }
            if (Msg5 != null)
            {
                yield return new WaitForSeconds(TimeUntilNextMsg);
                Msg5.SetActive(true);
            }
            if (Msg6 != null)
            {
                yield return new WaitForSeconds(TimeUntilNextMsg);
                Msg6.SetActive(true);
            }
            if (Msg7 != null)
            {
                yield return new WaitForSeconds(TimeUntilNextMsg);
                Msg7.SetActive(true);
            }
            if (Msg8 != null)
            {
                yield return new WaitForSeconds(TimeUntilNextMsg);
                Msg8.SetActive(true);
            }
            yield return new WaitForSeconds(TimeUntilNextMsg);
            yield return null;
        }
    }
}
