using System;
using Unity.Collections;
using Random = UnityEngine.Random;

public interface IAgent
{
    //PlayerID not used everywhere
    int Act(ref WindjermanGameState gs, NativeList<int> availableActions);
}

public struct RandomAgent : IAgent
{
    public Unity.Mathematics.Random rdm;
    
    public int Act(ref WindjermanGameState gs, NativeList<int> availableActions)
    {
        return availableActions[rdm.NextInt(0, availableActions.Length)];
    }
}