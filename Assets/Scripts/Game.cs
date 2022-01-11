using UnityEngine;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    public Board _board;
    public GameObject UIGameObject;
    public UI _ui;
    private double _gameStartTime;
    private bool _gameInProgress;

    public void OnClickedNewGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        if (_board != null)
        {
            _board.RechargeBoxes();
        }

        if (_ui != null)
        {
            _ui.HideMenu();
        }
    }

    public void OnClickedExit()
    {
#if !UNITY_EDITOR
        Application.Quit();
#endif
    }

    public void OnClickedReset()
    {
        Time.timeScale = 1;
        if (_board != null)
        {
            _board.Clear();
        }

        if (_ui != null)
        {
            _ui.HideResult();
            _ui.ShowMenu();
        }
    }

    public void RestartGame()
    {
        Debug.Log("restarted game");
        string sceneindex = SceneManager.GetActiveScene().path;
        SceneManager.LoadScene(sceneindex);
    }

    private void Awake()
    {
        _board = transform.GetChild(0).GetComponentInChildren<Board>();
        _ui = UIGameObject.GetComponent<UI>();
        _gameInProgress = false;
    }

    private void Start()
    {
        if (_board != null)
        {
            _board.Setup(BoardEvent);
        }

        if (_ui != null)
        {
            _ui.ShowMenu();
        }
    }

    private void BoardEvent(Board.Event eventType)
    {
        if (eventType == Board.Event.ClickedDanger && _ui != null)
        {
            _ui.ShowResult(success: false);
        }

        if (eventType == Board.Event.Win && _ui != null)
        {
            _ui.ShowResult(success: true);
        }

        if (!_gameInProgress)
        {
            _gameInProgress = true;
            _gameStartTime = Time.realtimeSinceStartupAsDouble;
        }
    }
}
