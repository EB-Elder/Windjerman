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

    private IAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        Rules.Init(ref gs);
        PlayerView1 = Instantiate(PlayerPrefab1).GetComponent<Transform>();
        PlayerView2 = Instantiate(PlayerPrefab2).GetComponent<Transform>();
        frisbeeView = Instantiate(FrisbeePrefab).GetComponent<Transform>();
        agent = new HumanPlayerAgent();
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
        Rules.Step(ref gs, agent.Act(ref gs, Rules.GetAvailableActions1(ref gs), 99), agent.Act(ref gs, Rules.GetAvailableActions2(ref gs), 99));
    }
}
