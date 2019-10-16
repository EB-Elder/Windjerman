using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

using Rules = WindjermanGameStateRules;

public class RandomRolloutAgent : IAgent
{
    private int PlayerID;
    public RandomRolloutAgent(int PlayerID)
    {
        this.PlayerID = PlayerID;
    }
    public int Act(ref WindjermanGameState gs, int[] availableActions)
    {
        var epochs = 10;
        var agent = new RandomAgent();

        var summedScores = new NativeArray<long>(availableActions.Length, Allocator.Temp);

        if (PlayerID == 0)
        {
            for (var i = 0; i < availableActions.Length; i++)
            {
                for (var n = 0; n < epochs; n++)
                {
                    var gsCopy = Rules.Clone(ref gs);

                
                    Rules.Step(ref gsCopy, availableActions[i], 0);

                    var currentDepth = 0;
                    while (!gsCopy.isGameOver)
                    {
                        Rules.Step(ref gsCopy, agent.Act(ref gsCopy, Rules.GetAvailableActions1(ref gsCopy)), 0);
                        currentDepth++;
                        if (currentDepth > 500)
                        {
                            break;
                        }
                    }

                    summedScores[i] += gsCopy.playerScore1;
                }
            }
        }
        else if (PlayerID == 1)
        {
            for (var i = 0; i < availableActions.Length; i++)
            {
                for (var n = 0; n < epochs; n++)
                {
                    var gsCopy = Rules.Clone(ref gs);

                
                    Rules.Step(ref gsCopy, 0, availableActions[i]);

                    var currentDepth = 0;
                    while (!gsCopy.isGameOver)
                    {
                        Rules.Step(ref gsCopy, 0, agent.Act(ref gsCopy, Rules.GetAvailableActions1(ref gsCopy)));
                        currentDepth++;
                        if (currentDepth > 500)
                        {
                            break;
                        }
                    }

                    summedScores[i] += gsCopy.playerScore2;
                }
            }
        }
        

        var bestActionIndex = -1;
        var bestScore = long.MinValue;
        for (var i = 0; i < summedScores.Length; i++)
        {
            if (bestScore > summedScores[i])
            {
                continue;
            }

            bestScore = summedScores[i];
            bestActionIndex = i;
        }
        
        summedScores.Dispose();

        return availableActions[bestActionIndex];
    }
    
    
}