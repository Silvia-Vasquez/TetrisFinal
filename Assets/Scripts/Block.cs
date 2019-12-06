using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private float previousTime;
    public float fallTime = 0.8f;

    void Start()
    {

    }

    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.position += new Vector3(1, 0, 0);

            if (CheckisValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {

                transform.position += new Vector3(-1, 0, 0);

            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            transform.position += new Vector3(-1, 0, 0);

            if (CheckisValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {

                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);

            if (CheckisValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {

                transform.Rotate(0, 0, -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            transform.position += new Vector3(0, -1, 0);
        }
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
        }


        if (CheckisValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
        }
            else
            {

                transform.position += new Vector3(0, 1, 0);

                FindObjectOfType<Game>().DeleteRow();

                if (FindObjectOfType<Game>().CheckifAboveGrid(this))
            {
                FindObjectOfType<Game>().GameOver();
            }

                enabled = false;

                FindObjectOfType<Game>().SpawnNextBlock();
         }
 
    }

    bool CheckisValidPosition()
    {
        foreach (Transform block in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(block.position);

            if (FindObjectOfType<Game>().CheckifInside(pos) == false)
            {
                return false;
            }

            if(FindObjectOfType<Game>().CheckifInside (pos)== false)
            {
                return false;
            }

            if(FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
 
    }

