using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Box : MonoBehaviour
{
    [SerializeField] private Color[] DangerColors = new Color[8];
    public Material mat;
    public Material ogmat;
    public bool Selected = false;
    public GameObject UI;
    public UI uiscript;
    public TextMeshPro _textDisplay;
    private Action<Box> _changeCallback;
    private MeshRenderer mr;
    public Dice dice;

    public int RowIndex { get; private set; }
    public int ColumnIndex { get; private set; }
    public int ID { get; private set; }
    public int DangerNearby { get; private set; }

    public bool IsDangerous;

    public void Start()
    {
        mr = GetComponent<MeshRenderer>();
        ogmat = GetComponent<MeshRenderer>().material;
    }

    public void Setup(int id, int row, int column)
    {
        ID = id;
        RowIndex = row;
        ColumnIndex = column;
    }

    public void Charge(int dangerNearby, bool danger, Action<Box> onChange)
    {
        _changeCallback = onChange;
        DangerNearby = dangerNearby;
        IsDangerous = danger;
        ResetState();
    }

    public void Reveal()
    {
        Selected = true;
        _textDisplay.gameObject.SetActive(true);
        return;
    }

    public void Update()
    {
        if (UI == null)
        {
            UI = GameObject.FindGameObjectWithTag("ToggleUI");
            uiscript = UI.GetComponent<UI>();
        }
        if (dice == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("Dice");
            dice = g.GetComponent<Dice>();
        }
    }

    public void StandDown()
    {
        if (ogmat != null)
        {
            mr.material = ogmat;
        }

        if (_textDisplay != null)
        {
            _textDisplay.gameObject.SetActive(false);
        }
    }

    public void OnClick(GameObject g)
    {
        if (g == null)
        {
            Debug.LogWarning("can't find obj OnClick");
            return;
        }
        if (dice.OnFour == true)
        {
            return;
        }
        if (IsDangerous && ogmat != null && SceneManager.GetActiveScene().name == "TwoPlayer")
        {
            uiscript.ShowSubResultTwoPlayer(g);
            mr.material = mat;
            uiscript.ShowRestart();
            uiscript.ShowResult(false);
        }

        if(IsDangerous && ogmat != null && SceneManager.GetActiveScene().name != "TwoPlayer")
        {
            mr.material = mat;
            uiscript.ShowRestart();
            uiscript.ShowResult(false);
        }
        else if (_textDisplay != null)
        {
            _textDisplay.gameObject.SetActive(true);
        }

        _changeCallback?.Invoke(this);
    }

    private void Awake()
    {
        _textDisplay = GetComponentInChildren<TextMeshPro>(true);

        ResetState();
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
    }

    private void ResetState()
    {
        if (ogmat != null)
        {
            mr.material = ogmat;
        }

        if (_textDisplay != null)
        {
            if (DangerNearby > 0)
            {
                _textDisplay.text = DangerNearby.ToString("D");
                _textDisplay.color = DangerColors[DangerNearby - 1];
            }
            else
            {
                _textDisplay.text = string.Empty;
            }

            _textDisplay.gameObject.SetActive(false);
        }
    }
}
