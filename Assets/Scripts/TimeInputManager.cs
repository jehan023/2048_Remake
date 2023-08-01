using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirectionTime
{
    Left, Right, Up, Down
}

public class TimeInputManager : MonoBehaviour
{
    private GameManagerTime gm;

    private void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManagerTime>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //right move
            gm.Move(MoveDirectionTime.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //left move
            gm.Move(MoveDirectionTime.Left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //up move
            gm.Move(MoveDirectionTime.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //down move
            gm.Move(MoveDirectionTime.Down);
        }
    }
}
