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

    private IAgent agentJ1;
    private IAgent agentJ2;

    bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        //Rules.Init(ref gs);
        //PlayerView1 = Instantiate(PlayerPrefab1).GetComponent<Transform>();
        //PlayerView2 = Instantiate(PlayerPrefab2).GetComponent<Transform>();
        //frisbeeView = Instantiate(FrisbeePrefab).GetComponent<Transform>();
        //agent = new HumanPlayerAgent();
    }

    //Fonction pour lancer la partie via l'interface
    public void StartGame(listeChoix choix1, listeChoix choix2)
    {
        //Initialisation des règles
        Rules.Init(ref gs);

        //instanciation des éléments graphiques
        PlayerView1 = Instantiate(PlayerPrefab1).GetComponent<Transform>();
        PlayerView2 = Instantiate(PlayerPrefab2).GetComponent<Transform>();
        frisbeeView = Instantiate(FrisbeePrefab).GetComponent<Transform>();

        //création des agents en fonction des choix effectués sur l'interface
        switch(choix1)
        {
            case listeChoix.HUMAN:

                agentJ1 = new HumanPlayerAgent(0);
                break;

            case listeChoix.RANDOM:

                agentJ1 = new RandomAgent();
                break;

            case listeChoix.RANDOMROLLOUT:

                Debug.Log("Agent pas implémenté");
                break;

            case listeChoix.MCTS:

                Debug.Log("Agent pas implémenté");
                break;

            case listeChoix.QLEARNING:

                Debug.Log("Agent pas implémenté");
                break;

            default:

                Debug.Log("Agent pas implémenté");
                break;
        }

        switch(choix2)
        {
            case listeChoix.HUMAN:

                agentJ2 = new HumanPlayerAgent(1);
                break;

            case listeChoix.RANDOM:

                agentJ2 = new RandomAgent();
                break;

            case listeChoix.RANDOMROLLOUT:

                Debug.Log("Agent pas implémenté");
                break;

            case listeChoix.MCTS:

                Debug.Log("Agent pas implémenté");
                break;

            case listeChoix.QLEARNING:

                Debug.Log("Agent pas implémenté");
                break;

            default:

                Debug.Log("Agent pas implémenté");
                break;
        }

        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gs.isGameOver)
        {
            return;
        }

        if (!gameStarted) return;

        PlayerView1.position = gs.playerPosition1;
        PlayerView2.position = gs.playerPosition2;
        frisbeeView.position = gs.frisbeePosition;
        Rules.Step(ref gs, agentJ1.Act(ref gs, Rules.GetAvailableActions1(ref gs)), agentJ2.Act(ref gs, Rules.GetAvailableActions2(ref gs)));
    }
}
