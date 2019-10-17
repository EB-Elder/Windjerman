using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class HumanPlayerAgent : IAgent
{
    private int PlayerID;

    public HumanPlayerAgent(int PlayerID)
    {
        this.PlayerID = PlayerID;
    }
    public int Act(ref WindjermanGameState gs, NativeList<int> availableActions)
    {

        if (PlayerID == 0)
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
        }
        
        else if (PlayerID == 1)
        {
            if (Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Z))
            {
                return 6;
            }
        
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Z))
            {
                return 5;
            }
        
            if (Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Z))
            {
                return 7;
            }
        
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.Z))
            {
                return 4;
            }
        
            if (Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.S))
            {
                return 3;
            }
        
            if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Z))
            {
                return 1;
            }
        
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.Z))
            {
                return 2;
            }
        }

        return 0;
    }
}
