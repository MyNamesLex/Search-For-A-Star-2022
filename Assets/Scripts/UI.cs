using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private CanvasGroup Menu;
    [SerializeField] private CanvasGroup Result;
    [SerializeField] private CanvasGroup Restart;
    [SerializeField] private TMP_Text ResultText;
    public TMP_Text SubResultTwoPlayer;

    private static readonly string[] ResultTexts = { "Game Over!", "You Win!" };
    private static readonly float AnimationTime = 0.5f;

    public void ShowMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StartCoroutine(ShowCanvas(Menu, 1.0f));
    }

    public void ShowResult(bool success)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if (ResultText != null)
        {
            ResultText.text = ResultTexts[success ? 1 : 0];
        }
        StartCoroutine(ShowCanvas(Result, 1.0f));
    }

    public void ShowSubResultTwoPlayer(GameObject g)
    {
        if (SceneManager.GetActiveScene().name == "TwoPlayer")
        {
            if (g.gameObject.name == "Dice2")
            {
                Debug.Log("Dice2");
                SubResultTwoPlayer.gameObject.SetActive(true);
                SubResultTwoPlayer.text = "Player One Wins!";
            }
            if (g.gameObject.name == "Dice")
            {
                Debug.Log("Dice");
                SubResultTwoPlayer.gameObject.SetActive(true);
                SubResultTwoPlayer.text = "Player Two Wins!";
            }
        }
        else
        {
            Debug.LogError("Called twoplayer result in singleplayer!");
        }
    }

    public void HideMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(ShowCanvas(Menu, 0.0f));
    }

    public void HideResult()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(ShowCanvas(Result, 0.0f));
    }

    public void ShowRestart()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StartCoroutine(ShowCanvas(Restart, 1.0f));
    }

    private void Awake()
    {
        if (Result != null)
        {
            Result.alpha = 0.0f;
            Result.interactable = false;
            Result.blocksRaycasts = false;
        }
    }

    private static string FormatTime(double seconds)
    {
        float m = Mathf.Floor((int)seconds / 60);
        float s = (float)seconds - (m * 60);
        string mStr = m.ToString("00");
        string sStr = s.ToString("00.000");
        return string.Format("{0}:{1}", mStr, sStr);
    }

    public IEnumerator ShowCanvas(CanvasGroup group, float target)
    {
        if (group != null)
        {
            float startAlpha = group.alpha;
            float t = 0.0f;
            group.interactable = true;
            group.blocksRaycasts = true;


            while (t < AnimationTime)
            {
                t = Mathf.Clamp(t + Time.deltaTime, 0.0f, AnimationTime);
                group.alpha = Mathf.SmoothStep(startAlpha, target, t / AnimationTime);
                yield return null;
            }
        }
    }
}
