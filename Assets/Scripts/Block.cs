using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private float previousTime = 0;
    public float fallTime = 1f;

    private float VerticalSpeed = 0.05f;
    private float HorizontalSpeed = 0.1f;

    private float VTimer = 0;
    private float HTimer = 0;

    void Start()
    {

    }

    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {

            if(HTimer < HorizontalSpeed)
            {
                HTimer += Time.deltaTime;
                return;
            }

            HTimer = 0;

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
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (HTimer < HorizontalSpeed)
            {
                HTimer += Time.deltaTime;
                return;
            }

            HTimer = 0;

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
        else if (Input.GetKey(KeyCode.DownArrow) || Time.time - previousTime  >= fallTime) {

            if (VTimer < VerticalSpeed)
            {
                VTimer += Time.deltaTime;
                return;
            }

            VTimer = 0;

            transform.position += new Vector3(0, -1, 0);

            // }
            // if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
            //{
            //  transform.position += new Vector3(0, -1, 0);
            //previousTime = Time.time;
            //}


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
            previousTime = Time.time;

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

