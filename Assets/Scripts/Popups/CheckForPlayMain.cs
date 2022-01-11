using UnityEngine;

public class CheckForPlayMain : MonoBehaviour
{
    public static CheckForPlayMain instance = null;
    public bool HasPlayedSinglePlayer = false;
    public bool HasPlayedTwoPlayer = false;
    public bool SeenIntro = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }
}
