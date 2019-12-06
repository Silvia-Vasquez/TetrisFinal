using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    public int OneLine = 40;
    public int TwoLine = 100;
    public int ThreeLine = 300;
    public int FourLine = 1200;

    public Text Hud_score;

    private int numRowsThisTurn = 0;

    private int CurrentScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNextBlock();
    }

    public bool CheckifAboveGrid (Block block)
    {
        for (int x = 0; x<gridWidth; x++)
        {
            foreach (Transform blocks in block.transform)
            {
                Vector2 pos = Round(blocks.position);
                if (pos.y >gridHeight - 1)
                {
                    return true;
                }
            }
        }

        return false;
    }


    void Update()
    {
        UpdateScore();
        UpdateUI();
    }

    public void UpdateUI()
    {
        Hud_score.text = CurrentScore.ToString();
    }

    public void UpdateScore()
    {
        if (numRowsThisTurn > 0)
        {
            if (numRowsThisTurn == 1) {

                ClearOneLine();
            }
            else if (numRowsThisTurn == 2) {

                ClearTwoLines();

            } else if (numRowsThisTurn == 3) {

                ClearThreeLines();

            } else if (numRowsThisTurn == 4) {

                ClearFourLines();
            }
            numRowsThisTurn = 0;
        }
    }
    public void ClearOneLine()
    {
        CurrentScore += OneLine;
    }

    public void ClearTwoLines()
    {
        CurrentScore += TwoLine;
    }

    public void ClearThreeLines()
    {
        CurrentScore += ThreeLine;
    }

    public void ClearFourLines()
    {
        CurrentScore += FourLine;
    }

        
    public bool IsRowFull(int y)
    {
        for (int x=0; x<gridWidth; x++)
        {
            if (grid[x,y] == null)
            {
                return false;
            }
        }

        numRowsThisTurn++;

        return true;
    }

    public void DeleteBlock (int y)
    {
        for (int x=0; x<gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);

            grid[x, y] = null;
        }
    }

    public void MoveRowDown (int y) 
    {
        for (int x = 0; x<gridWidth; x++)
        {
            if (grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }   
    }

    public void MoveAllRowsDown (int y)
    {
        for(int i =y; i < gridHeight; i++)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for (int y=0; y< gridHeight; y++)
        {
            if (IsRowFull(y))
            {
                DeleteBlock(y);
                MoveAllRowsDown(y + 1);
                y--;
            }
        }
    }

    public void UpdateGrid(Block block)
    {
        for(int y=0; y <gridHeight; y++)
        {
            for (int x=0; x < gridWidth; x++)
            {
                if (grid[x,y] != null)
                {
                    if (grid[x,y].parent == block.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform blocks in block.transform)
        {

            Vector2 pos = Round(blocks.position);
            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = blocks;
            }
        }
    }

    public Transform GetTransformAtGridPosition (Vector2 pos)
    {

        if(pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }
    public void SpawnNextBlock()
    {
        GameObject nextBlock = (GameObject)Instantiate(Resources.Load(GetRandomBlock(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
    }

    public bool CheckifInside(Vector2 pos) {

            return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
        }
    public Vector2 Round (Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomBlock()
    {
        int randomBlock = Random.Range(1, 8);

        string randomBlockName = "Prefabs/JBlock";

        switch (randomBlock)
        {
            case 1:
                randomBlockName = "Prefabs/LBlock";
                break;
            case 2:
                randomBlockName = "Prefabs/LongBlock";
                break;
            case 3:
                randomBlockName = "Prefabs/SBlock";
                break;
            case 4:
                randomBlockName = "Prefabs/SquareBlock";
                break;
            case 5:
                randomBlockName = "Prefabs/TBlock";
                break;
            case 6:
                randomBlockName = "Prefabs/ZBlock";
                break;
            case 7:
                randomBlockName = "Prefabs/JBlock";
                break;
        }

        return randomBlockName;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    }

   

