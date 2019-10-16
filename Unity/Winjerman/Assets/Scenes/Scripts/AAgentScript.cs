using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;
using Rules = WindjermanGameStateRules;

public struct NodeAStar
{
    public WindjermanGameState gsNode;
    public float distanceFromFrisbee;
    public int playerID;

    public void CalculerDistanceFromFrisbee()
    {
        if(playerID == 0)
        {
            distanceFromFrisbee = Vector2.Distance(gsNode.playerPosition1, gsNode.frisbeePosition);
        }
        else
        {
            distanceFromFrisbee = Vector2.Distance(gsNode.playerPosition2, gsNode.frisbeePosition);
        }
    }

    public float getDistanceFromFrisbee()
    {
        return distanceFromFrisbee;
    }
}

public class AAgentScript : IAgent
{
    private int playerID;

    public AAgentScript(int playerID)
    {
        this.playerID = playerID;
    }

    public int Act(ref WindjermanGameState gs, int[] availableActions)
    {
        //création de la liste des nodes
        var listeNodes = new NativeArray<NodeAStar>();

        //pour chaque action disponible
        for (int i = 0; i < availableActions.Length; i++)
        {
            //on crée le node
            NodeAStar n = new NodeAStar();
            n.distanceFromFrisbee = 0;
            n.playerID = this.playerID;
            n.gsNode = Rules.Clone(ref gs);
        }

        return 0;
    }
}
