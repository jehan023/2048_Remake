using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection5x5
{
    Left, Right, Up, Down
}

public class InputManager5x5 : MonoBehaviour
{
    private GameManager5x5 gm;

    private void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager5x5>();
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
            gm.Move(MoveDirection5x5.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //left move
            gm.Move(MoveDirection5x5.Left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //up move
            gm.Move(MoveDirection5x5.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //down move
            gm.Move(MoveDirection5x5.Down);
        }
    }
}
