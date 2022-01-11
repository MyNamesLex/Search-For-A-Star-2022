using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    public enum Event { ClickedBlank, ClickedNearDanger, ClickedDanger, Win };

    [SerializeField] public Box BoxPrefab;
    [SerializeField] public int Width = 10;
    [SerializeField] public int Height = 10;
    [SerializeField] private float Gap;
    [SerializeField] private int ZScale;
    [SerializeField] public int NumberOfDangerousBoxes = 10;
    public bool isSecondPlayerBoard = false;
    public int neighbourIndex;

    public Box[] _grid;
    private Transform _rect;
    public Vector3Int[] _neighbours;
    private Action<Event> _clickEvent;
    public TimeLineManager tm;
    public void Setup(Action<Event> onClickEvent)
    {
        _clickEvent = onClickEvent;
        Clear();
    }

    public void Clear()
    {
        for (int row = 0; row < Height; ++row)
        {
            for (int column = 0; column < Width; ++column)
            {
                int index = row * Width + column;
                _grid[index].StandDown();
            }
        }
    }

    public void RechargeBoxes()
    {
        int numberOfItems = Width * Height;
        List<bool> dangerList = new List<bool>(numberOfItems);

        for (int count = 0; count < numberOfItems; ++count)
        {
            dangerList.Add(count < NumberOfDangerousBoxes);
        }

        dangerList.RandomShuffle();

        for (int row = 0; row < Height; ++row)
        {
            for (int column = 0; column < Width; ++column)
            {
                int index = row * Width + column;
                _grid[index].Charge(CountDangerNearby(dangerList, index), dangerList[index], OnClickedBox);
            }
        }
    }

    private void Awake()
    {
        _grid = new Box[Width * Height];
        _rect = transform as Transform;
        Transform boxRect = BoxPrefab.transform;

        _rect.transform.position = new Vector3(boxRect.transform.position.x * Width, boxRect.transform.position.y * Height, boxRect.transform.position.z);
        Vector3 startPosition = _rect.position - (_rect.localScale * 0.5f) + (boxRect.localScale * 1.5f);
        startPosition.y *= -1.0f;
        if(isSecondPlayerBoard == true)
        {
            startPosition.x *= 123f;
        }

        _neighbours = new Vector3Int[8] // transferred to V3 to find neighbours in 3D, int Z placeholder
        {
            new Vector3Int(-Width - 1, -1, 0),
            new Vector3Int(-Width, -1, 0),
            new Vector3Int(-Width + 1, -1, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(Width - 1, 1, 0),
            new Vector3Int(Width, 1 , 0),
            new Vector3Int(Width + 1, 1, 0)
        };

        for (int row = 0; row < Width; ++row)
        {
            GameObject rowObj = new GameObject(string.Format("Row{0}", row));
            Transform rowRect = rowObj.transform;
            rowRect.SetParent(transform);
            rowRect.tag = "BoardPiece";
            rowRect.gameObject.layer = 3;
            rowRect.transform.position = new Vector3(transform.position.x, startPosition.y * row, transform.position.z);
            rowRect.localScale = new Vector3(boxRect.transform.localScale.x * Width, boxRect.transform.localScale.y * Height, boxRect.transform.localScale.z * ZScale);

            for (int column = 0; column < Height; ++column)
            {
                int index = row * Width + column;
                _grid[index] = Instantiate(BoxPrefab, rowObj.transform);
                _grid[index].Setup(index, row, column);
                Transform gridBoxTransform = _grid[index].transform;
                _grid[index].tag = "BoardPiece";
                _grid[index].name = string.Format("ID{0}, Row{1}, Column{2}", index, row, column);
                gridBoxTransform.transform.position = new Vector3(startPosition.x + (boxRect.localScale.x * column) * Gap, 0, startPosition.z + (boxRect.localScale.y * row) * Gap);
            }
        }

        // Sanity check
        for (int count = 0; count < _grid.Length; ++count)
        {
            //Debug.LogFormat("Count: {0}  ID: {1}  Row: {2}  Column: {3}", count, _grid[count].ID, _grid[count].RowIndex, _grid[count].ColumnIndex);
        }
    }

    public int CountDangerNearby(List<bool> danger, int index)
    {
        int result = 0;
        int boxRow = index / Width;

        if (!danger[index])
        {
            for (int count = 0; count < _neighbours.Length; ++count)
            {
                int neighbourIndex = index + _neighbours[count].x;
                int expectedRow = boxRow + _neighbours[count].y;
                int neighbourRow = neighbourIndex / Width;
                result += (expectedRow == neighbourRow && neighbourIndex >= 0 && neighbourIndex < danger.Count && danger[neighbourIndex]) ? 1 : 0;
            }
        }

        return result;
    }

    public void OnClickedBox(Box box)
    {
        Event clickEvent = Event.ClickedBlank;
        if (box.IsDangerous)
        {
            tm.StartSharkCutscene();
            clickEvent = Event.ClickedDanger;
        }
        else if (box.DangerNearby > 0)
        {
            clickEvent = Event.ClickedNearDanger;
        }
        else
        {
            ClearNearbyBlanks(box);
        }

        if (CheckForWin())
        {
            clickEvent = Event.Win;
        }

        _clickEvent?.Invoke(clickEvent);
    }

    private bool CheckForWin()
    {
        bool Result = true;
        for (int count = 0; Result && count < _grid.Length; ++count)
        {
            if (_grid[count].IsDangerous == true && _grid[count])
            {
                Result = false;
            }
        }

        return Result;
    }

    private void ClearNearbyBlanks(Box box)
    {
        RecursiveClearBlanks(box);
    }

    private void RecursiveClearBlanks(Box box)
    {
        if (!box.IsDangerous)
        {
            box.Reveal();

            if (box.DangerNearby == 0)
            {
                for (int count = 0; count < _neighbours.Length; ++count)
                {
                    neighbourIndex = box.ID + _neighbours[count].x;
                    int expectedRow = box.RowIndex + _neighbours[count].y;
                    int neighbourRow = neighbourIndex / Width;
                    bool correctRow = expectedRow == neighbourRow;
                    bool active = neighbourIndex >= 0 && neighbourIndex < _grid.Length && _grid[neighbourIndex];

                    if (correctRow && active)
                    {
                        //RecursiveClearBlanks(_grid[neighbourIndex]); // causes stack overflow error
                        //ClearNearbyBlanks(_grid[neighbourIndex]); // causes stack overflow error
                        //Debug.Log(_grid[neighbourIndex]);
                        box.transform.GetChild(1).GetComponent<TextMeshPro>().text = "Clear";
                        _grid[neighbourIndex].DeActivate();
                    }
                }
            }
        }
    }
}
