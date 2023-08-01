using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerTime : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject YouWinPanel;
    public GameObject BestTimePanel;
    public Text NewBestTimeText;

    private Tile[,] AllTiles = new Tile[4, 4];
    private List<Tile[]> columns = new List<Tile[]>();
    private List<Tile[]> rows = new List<Tile[]>();
    private List<Tile> EmptyTiles = new List<Tile>();
    bool stillMove = true;

    // Start is called before the first frame update
    void Start()
    {
        Tile[] AllTileOneDim = GameObject.FindObjectsOfType<Tile>();
        foreach (Tile t in AllTileOneDim)
        {
            t.Number = 0;
            AllTiles[t.indRow, t.indCol] = t;
            EmptyTiles.Add(t);
        }

        columns.Add(new Tile[] { AllTiles[0, 0], AllTiles[1, 0], AllTiles[2, 0], AllTiles[3, 0] });
        columns.Add(new Tile[] { AllTiles[0, 1], AllTiles[1, 1], AllTiles[2, 1], AllTiles[3, 1] });
        columns.Add(new Tile[] { AllTiles[0, 2], AllTiles[1, 2], AllTiles[2, 2], AllTiles[3, 2] });
        columns.Add(new Tile[] { AllTiles[0, 3], AllTiles[1, 3], AllTiles[2, 3], AllTiles[3, 3] });

        rows.Add(new Tile[] { AllTiles[0, 0], AllTiles[0, 1], AllTiles[0, 2], AllTiles[0, 3] });
        rows.Add(new Tile[] { AllTiles[1, 0], AllTiles[1, 1], AllTiles[1, 2], AllTiles[1, 3] });
        rows.Add(new Tile[] { AllTiles[2, 0], AllTiles[2, 1], AllTiles[2, 2], AllTiles[2, 3] });
        rows.Add(new Tile[] { AllTiles[3, 0], AllTiles[3, 1], AllTiles[3, 2], AllTiles[3, 3] });

        Generate();
        Generate();
    }

    private void YouWin()
    {
        TimeTracker.Instance.start = false;
        
        if (TimeTracker.Instance.time < PlayerPrefs.GetFloat("BestTime"))
        {
            TimeTracker.Instance.UpdateBestTime(TimeTracker.Instance.time);
            NewBestTimeText.text = TimeTracker.Instance.TimeText.text;
            BestTimePanel.SetActive(true);
        }
        else
        {
            TimeTracker.Instance.UpdateBestTime(TimeTracker.Instance.time);
            YouWinPanel.SetActive(true);
        }
            
    }

    private void GameOver()
    {
        TimeTracker.Instance.start = false;
        GameOverPanel.SetActive(true);
    }

    bool CanMove()
    {
        if (EmptyTiles.Count > 0)
            return true;
        else
        {
            //CHECK COLUMNS
            for (int c = 0; c < columns.Count; c++)
                for (int r = 0; r < rows.Count - 1; r++)
                    if (AllTiles[r, c].Number == AllTiles[r + 1, c].Number)
                        return true;

            //CHECK ROWS
            for (int r = 0; r < rows.Count; r++)
                for (int c = 0; c < columns.Count - 1; c++)
                    if (AllTiles[r, c].Number == AllTiles[r, c + 1].Number)
                        return true;
        }
        return false;
    }


    public void NewGameButtonHandler()
    {
        SceneManager.LoadScene("TimeGameScene");
    }
    public void MainMenuButtonHandler()
    {
        SceneManager.LoadScene("MainMenu");
    }


    bool MakeOneMoveDownIndex(Tile[] LineOfTiles)
    {
        for (int i = 0; i < LineOfTiles.Length - 1; i++)
        {
            //MOVE BLOCK
            if (LineOfTiles[i].Number == 0 && LineOfTiles[i + 1].Number != 0)
            {
                LineOfTiles[i].Number = LineOfTiles[i + 1].Number;
                LineOfTiles[i + 1].Number = 0;
                return true;
            }
            // MERGE BLOCK
            if (LineOfTiles[i].Number != 0 && LineOfTiles[i].Number == LineOfTiles[i + 1].Number &&
                LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i + 1].mergedThisTurn == false)
            {
                LineOfTiles[i].Number *= 2;
                LineOfTiles[i + 1].Number = 0;
                LineOfTiles[i].mergedThisTurn = true;
                LineOfTiles[i].PlayMergeAnimation();
                if (LineOfTiles[i].Number == 2048)
                {
                    stillMove = false;
                    YouWin();
                }
                return true;
            }
        }
        return false;
    }
    bool MakeOneMoveUpIndex(Tile[] LineOfTiles)
    {
        for (int i = LineOfTiles.Length - 1; i > 0; i--)
        {
            //MOVE BLOCK
            if (LineOfTiles[i].Number == 0 && LineOfTiles[i - 1].Number != 0)
            {
                LineOfTiles[i].Number = LineOfTiles[i - 1].Number;
                LineOfTiles[i - 1].Number = 0;
                return true;
            }
            // MERGE BLOCK
            if (LineOfTiles[i].Number != 0 && LineOfTiles[i].Number == LineOfTiles[i - 1].Number &&
                LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i - 1].mergedThisTurn == false)
            {
                LineOfTiles[i].Number *= 2;
                LineOfTiles[i - 1].Number = 0;
                LineOfTiles[i].mergedThisTurn = true;
                LineOfTiles[i].PlayMergeAnimation();
                if (LineOfTiles[i].Number == 2048)
                {
                    stillMove = false;
                    YouWin();
                }
                return true;
            }
        }
        return false;
    }

    void Generate()
    {
        if (EmptyTiles.Count > 0)
        {
            int indexForNewNumber = Random.Range(0, EmptyTiles.Count);
            int randomNum = Random.Range(0, 10);
            if (randomNum == 0)
                EmptyTiles[indexForNewNumber].Number = 4;
            else
                EmptyTiles[indexForNewNumber].Number = 2;
            EmptyTiles[indexForNewNumber].PlayAppearAnimation();
            EmptyTiles.RemoveAt(indexForNewNumber);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ResetMergedFlags()
    {
        foreach (Tile t in AllTiles)
            t.mergedThisTurn = false;
    }
    private void UpdateEmptyTiles()
    {
        EmptyTiles.Clear();
        foreach (Tile t in AllTiles)
        {
            if (t.Number == 0)
                EmptyTiles.Add(t);
        }
    }

    public void Move(MoveDirectionTime md)
    {
        Debug.Log(md.ToString() + " move.");
        bool moveMade = false;
        ResetMergedFlags();

        if (stillMove)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                switch (md)
                {
                    case MoveDirectionTime.Down:
                        while (MakeOneMoveUpIndex(columns[i]))
                        {
                            moveMade = true;
                        }
                        break;
                    case MoveDirectionTime.Left:
                        while (MakeOneMoveDownIndex(rows[i]))
                        {
                            moveMade = true;
                        }
                        break;
                    case MoveDirectionTime.Right:
                        while (MakeOneMoveUpIndex(rows[i]))
                        {
                            moveMade = true;
                        }
                        break;
                    case MoveDirectionTime.Up:
                        while (MakeOneMoveDownIndex(columns[i]))
                        {
                            moveMade = true;
                        }
                        break;
                }
            }
            if (moveMade)
            {
                UpdateEmptyTiles();
                Generate();

                if (!CanMove())
                {
                    GameOver();
                }
            }
        }

    }
}
