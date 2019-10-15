using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerAgent : IAgent
{
    public int Act(ref WindjermanGameState gs, int[] availableActions)
    {
        
        if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            return 6;
        }
        
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            return 5;
        }
        
        if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            return 7;
        }
        
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            return 4;
        }
        
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            return 3;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            return 1;
        }
        
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            return 2;
        }

        return 0;
    }
}
