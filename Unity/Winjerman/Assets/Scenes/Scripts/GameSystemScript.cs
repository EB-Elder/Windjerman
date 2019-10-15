using System;
using System.Collections.Generic;
using UnityEngine;
using Rules = WindjermanGameStateRules;
public class GameSystemScript : MonoBehaviour
{
    public GameObject PlayerPrefab1;
    public GameObject PlayerPrefab2;
    public GameObject FrisbeePrefab;

    private WindjermanGameState gs;
    
    private Transform frisbeeView;
    private Transform PlayerView1;
    private Transform PlayerView2;

    private IAgent agent1;
    private IAgent agent2;

    
    // Start is called before the first frame update
    void Start()
    {
        Rules.Init(ref gs);
        PlayerView1 = Instantiate(PlayerPrefab1).GetComponent<Transform>();
        PlayerView2 = Instantiate(PlayerPrefab2).GetComponent<Transform>();
        frisbeeView = Instantiate(FrisbeePrefab).GetComponent<Transform>();
        //PlayerID 0 => Joueur 1
        //PlayerID 1 => Joueur 2
        agent1 = new HumanPlayerAgent(1);
        agent2 = new RandomAgent();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gs.isGameOver)
        {
            return;
        }

        PlayerView1.position = gs.playerPosition1;
        PlayerView2.position = gs.playerPosition2;
        frisbeeView.position = gs.frisbeePosition;
        Rules.Step(ref gs, agent1.Act(ref gs, Rules.GetAvailableActions1(ref gs)), agent2.Act(ref gs, Rules.GetAvailableActions2(ref gs)));
    }
}
