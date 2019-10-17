using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

using Rules = WindjermanGameStateRules;

public class RandomRolloutAgent : IAgent
{
    private int PlayerID;
    
    
    public RandomRolloutAgent(int ID)
    {
        PlayerID = ID;
    }

    [BurstCompile]
    struct RandomRolloutJob : IJobParallelFor
    {
        
        public WindjermanGameState gs;

        public int Playerid;

        [ReadOnly]
        public NativeList<int> availableActions;
        
        [ReadOnly]
        public NativeList<int> availableActionsFrozen;
        
        [ReadOnly]
        public NativeList<int> availableActionsFree;
        
        [ReadOnly]
        public NativeList<int> availableActionsStun;

        public RandomAgent rdmAgent;

        [WriteOnly]
        public NativeArray<long> summedScores;
        
        public void Execute(int index)
        {
            var epochs = 10;
            var agent = rdmAgent;


            if (Playerid == 0)
            {
                for (var n = 0; n < epochs; n++)
                    {
                        var gsCopy = Rules.Clone(ref gs);

                
                        Rules.Step(ref gsCopy, availableActions[index], 0);

                        var currentDepth = 0;
                        while (!gsCopy.isGameOver)
                        {
                            var possibleChoice = Rules.GetAvailableActions1int(ref gsCopy);
                            switch (possibleChoice)
                            {
                                case 0:
                                    Rules.Step(ref gsCopy, agent.Act(ref gsCopy, availableActionsFree), 0);
                                    break;
                                    
                                case 1:
                                    Rules.Step(ref gsCopy, agent.Act(ref gsCopy, availableActionsFrozen), 0);
                                    break;
                                
                                case 2:
                                    Rules.Step(ref gsCopy, agent.Act(ref gsCopy, availableActionsStun), 0);
                                    break;
                                    
                            }
                            
                            currentDepth++;
                            if (currentDepth > 500)
                            {
                                break;
                            }
                        }

                        summedScores[index] += gsCopy.playerScore1;
                    }
                
            }
            else if (Playerid == 1)
            {
                
                    for (var n = 0; n < epochs; n++)
                    {
                        var gsCopy = Rules.Clone(ref gs);

                
                        Rules.Step(ref gsCopy, 0, availableActions[index]);

                        var currentDepth = 0;
                        while (!gsCopy.isGameOver)
                        {
                            var possibleChoice = Rules.GetAvailableActions2int(ref gsCopy);
                            switch (possibleChoice)
                            {
                                case 0:
                                    Rules.Step(ref gsCopy, 0, agent.Act(ref gsCopy, availableActionsFree));
                                    break;
                                    
                                case 1:
                                    Rules.Step(ref gsCopy, 0,agent.Act(ref gsCopy, availableActionsFrozen));
                                    break;
                                
                                case 2:
                                    Rules.Step(ref gsCopy, 0, agent.Act(ref gsCopy, availableActionsStun));
                                    break;
                                    
                            }
                            currentDepth++;
                            if (currentDepth > 500)
                            {
                                break;
                            }
                        }

                        summedScores[index] += gsCopy.playerScore2;
                    }
                }
            
        }
    }
    
    public int Act(ref WindjermanGameState gs, NativeList<int> availableActions)
    {
        var job = new RandomRolloutJob
        {
            availableActions = availableActions,
            availableActionsFree = Rules.AvailableActionsFree,
            availableActionsStun = Rules.AvailableActionsStun,
            availableActionsFrozen = Rules.AvailableActionsFrozen,
            gs = gs,
            summedScores = new NativeArray<long>(availableActions.Length, Allocator.TempJob),
            rdmAgent = new RandomAgent {rdm = new Unity.Mathematics.Random((uint) Time.frameCount)},
            Playerid = PlayerID
            
        };

        var handle = job.Schedule(availableActions.Length, 1);
        handle.Complete();
        

        var bestActionIndex = -1;
        var bestScore = long.MinValue;
        for (var i = 0; i < job.summedScores.Length; i++)
        {
            if (bestScore > job.summedScores[i])
            {
                continue;
            }

            bestScore = job.summedScores[i];
            bestActionIndex = i;
        }
        
        var chosenAction = availableActions[bestActionIndex];
        job.summedScores.Dispose();

        return chosenAction;
    }
    
    
}