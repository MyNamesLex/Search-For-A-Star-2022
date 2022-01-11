using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Popups : MonoBehaviour
{
    public bool ShowPopups = false;
    public bool Remove = false;
    private bool stopdupe = false;

    public string[] PopupText;
    public TextMeshProUGUI text;
    public Image PopupImage;
    public GameObject PopupObj;
    public float DisappearTimerDuration;
    public Animator anim;
    public int TextStages;

    public void Awake()
    {
        anim.SetBool("Hide", true);
    }

    private void Update()
    {
        if (ShowPopups == true && stopdupe == false)
        {
            stopdupe = true;
            ShowPopupVoid();
        }
        if(Remove == true)
        {
            PopupObj.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void ShowPopupVoid()
    {
        if (stopdupe == true)
        {
            PopUpTextStages();
            StartCoroutine(DisappearTimer());
        }
    }

    IEnumerator DisappearTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(DisappearTimerDuration);
            TextStages++;
            PopUpTextStages();
            anim.SetBool("Hide", false);
            yield return new WaitForSeconds(DisappearTimerDuration);
            anim.SetBool("Hide", true);
        }
    }

    public void PopUpTextStages()
    {
        switch (TextStages)
        {
            case 0:
                text.text = PopupText[0];
                break;
            case 1:
                text.text = PopupText[1];
                break;
            case 2:
                text.text = PopupText[2];
                break;
            case 3:
                text.text = PopupText[3];
                break;
            case 4:
                text.text = PopupText[4];
                break;
            case 5:
                text.text = PopupText[5];
                break;
            case 6:
                if (SceneManager.GetActiveScene().name == "Main")
                {
                    text.text = PopupText[6];
                    break;
                }
                else
                {
                    StartCoroutine(End());
                    break;
                }

            default:
                StartCoroutine(End());
                break;
        }
    }

    IEnumerator End()
    {
        while(true)
        {
            yield return new WaitForSeconds(DisappearTimerDuration);
            PopupObj.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
