using System;
using Random = UnityEngine.Random;

public interface IAgent
{
    //PlayerID not used everywhere
    int Act(ref WindjermanGameState gs, int[] availableActions, int PlayerID);
}

public class RandomAgent : IAgent
{
    public int Act(ref WindjermanGameState gs, int[] availableActions, int PlayerID)
    {
        return availableActions[Random.Range(0, availableActions.Length)];
    }
}