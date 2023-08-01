using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection 
{
    Left, Right, Up, Down
}

public class InputManager : MonoBehaviour
{
    private GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
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
            gm.Move(MoveDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //left move
            gm.Move(MoveDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //up move
            gm.Move(MoveDirection.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //down move
            gm.Move(MoveDirection.Down);
        }
    }
}
