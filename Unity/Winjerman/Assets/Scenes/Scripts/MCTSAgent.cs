using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Rules = WindjermanGameStateRules;


public class Node
{
    public Node NodeParent;
    public List<Node> NodeChild;
    public WindjermanGameState currentGs;
    public int numberSelected;// Nombre de fois ou cette action a était séléctionner;
    public int numberConsidered; // Nombre de fois ou cette action aurait pu être séléctionner
    
}


public class MCTSAgent : IAgent
{
    private int PlayerID;
    private Node[] nodes;

    public MCTSAgent(int PlayerID)
    {
        this.PlayerID = PlayerID;
    }
    public int Act(ref WindjermanGameState gs, int[] availableActions)
    {
        
        //var epochs = 5;
        var currentDepth = 0;
        
        var summedScores = new NativeArray<long>(availableActions.Length, Allocator.Temp);

        var gsCopy = Rules.Clone(ref gs);
        
        if (PlayerID == 0)
        {
            {
                return ReturnBestChoice(ref gsCopy, ref currentDepth, availableActions);       
            }    
        }
        else if (PlayerID == 1)
        {
            
        }
        
        return 0;
    }

    private int ReturnBestChoice(ref WindjermanGameState gsCopied ,ref int currentDepth, int[] availableActions)
    {

        
        Node parent = new Node();
        parent.NodeChild = new List<Node>();
        
        
        var finalScore = -99;
        if (currentDepth > 5)
        {
            for (var i = 0; i < availableActions.Length; i++)
            {
                Node child = new Node();
                child.NodeParent = parent;
                child.currentGs = gsCopied;
                parent.NodeChild.Add(child);
                var gsCopy = gsCopied;
                Rules.Step(ref gsCopy, availableActions[i], 0);
                
            }
        }
        
        return 0;
    }
}
