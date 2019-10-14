using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerAgent : IAgent
{
    public int Act(ref WindjermanGameState gs, int[] availableActions, int PlayerID)
    {
        if (gs.isFreeze1 == true || PlayerID == 0)
        {
            
        }
        
        if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            return 10;
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
        
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            return 5;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            return 7;
        }
        
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            return 6;
        }
        
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            return 8;
        }
        
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            return 11;
        }
        
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            return 9;
        }

        return 0;
    }
}
