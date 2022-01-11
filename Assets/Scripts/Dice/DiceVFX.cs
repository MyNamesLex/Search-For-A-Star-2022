using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DiceVFX : MonoBehaviour
{
    public TextMeshProUGUI DiceLandText;
    public Dice dicescript;
    public ParticleSystem DiceEffect1VFX;
    public ParticleSystem DiceEffect2VFX;
    public ParticleSystem DiceEffect3VFX;
    public ParticleSystem DiceEffect4VFX;
    public ParticleSystem DiceEffect5VFX;
    public ParticleSystem DiceEffect6VFX;
    public ParticleSystem DiceNudgeEffectVFX;

    IEnumerator DiceLanded(int side, string text)
    {
        while(true)
        {
            if (DiceLandText.isActiveAndEnabled == false)
            {
                DiceLandText.enabled = true;
                DiceLandText.text = text + " " + side;
                if (text == "Landed on a dangerous square but the dice landed on")
                {
                    yield return new WaitForSeconds(5);
                }
                else
                {
                    yield return new WaitForSeconds(1);
                }
                DiceLandText.enabled = false;
                yield break;
            }
        }
    }

    public void DiceEffectOne()
    {
        StartCoroutine(DiceLanded(1, "Landed on"));
        DiceEffect1VFX.transform.position = transform.position;
        DiceEffect1VFX.Play();
    }

    public void DiceEffectTwo()
    {
        StartCoroutine(DiceLanded(2, "Landed on"));
        DiceEffect2VFX.transform.position = transform.position;
        DiceEffect2VFX.Play();
    }

    public void DiceEffectThree()
    {
        StartCoroutine(DiceLanded(3, "Landed on"));
        DiceEffect3VFX.transform.position = transform.position;
        DiceEffect3VFX.Play();
    }

    public void DiceEffectFour()
    {
        StartCoroutine(DiceLanded(4, "Landed on"));
        DiceEffect4VFX.transform.position = transform.position;
        DiceEffect4VFX.Play();
    }

    public void DiceEffectFive()
    {
        StartCoroutine(DiceLanded(5, "Landed on"));
        DiceEffect5VFX.transform.position = transform.position;
        DiceEffect5VFX.Play();
    }

    public void DiceEffectSix(GameObject g)
    {
        if (g.GetComponent<Box>().IsDangerous == false)
        {
            StartCoroutine(DiceLanded(6, "Landed on"));
        }
        else
        {
            StartCoroutine(DiceLanded(6, "Landed on a dangerous square but the dice landed on"));
        }
        DiceEffect6VFX.transform.position = transform.position;
        DiceEffect6VFX.Play();
    }

    public void DiceNudgeEffect()
    {
        DiceNudgeEffectVFX.transform.position = transform.position;
        DiceNudgeEffectVFX.Play();
    }
}
