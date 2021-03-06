﻿using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Rules = WindjermanGameStateRules;
public class GameSystemScript : MonoBehaviour
{
    public GameObject PlayerPrefab1;
    public GameObject PlayerPrefab2;
    public GameObject FrisbeePrefab;

    public InterfaceManagerScript IMS;

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
        switch (choix1)
        {
            case listeChoix.HUMAN:

                agentJ1 = new HumanPlayerAgent(0);
                break;

            case listeChoix.RANDOM:

                agentJ1 = new RandomAgent {
                    rdm = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(0, 100))
                };
                break;

            case listeChoix.RANDOMROLLOUT:

                agentJ1 = new RandomRolloutAgent(0);
                break;

            case listeChoix.DIJKSTRA:

                agentJ1 = new AAgentScript(0);
                break;

            case listeChoix.MCTS:

                Debug.Log("Agent pas implémenté");
                break;

            default:

                Debug.Log("Agent pas implémenté");
                break;
        }

        switch (choix2)
        {
            case listeChoix.HUMAN:

                agentJ2 = new HumanPlayerAgent(1);
                break;

            case listeChoix.RANDOM:

                agentJ2 = new RandomAgent
                {
                    rdm = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(0, 100))
                }; 
                break;

            case listeChoix.RANDOMROLLOUT:

                agentJ2 = new RandomRolloutAgent(1);
                break;

            case listeChoix.DIJKSTRA:

                agentJ2 = new AAgentScript(1);
                break;

            case listeChoix.MCTS:

                Debug.Log("Agent pas implémenté");
                break;

            default:

                Debug.Log("Agent pas implémenté");
                break;
        }

        gameStarted = true;
        StartCoroutine(TimerGame(gs.dureePartie));
    }


    //gestion du timer
    public IEnumerator TimerGame(int nbSec)
    {
        for(int i = 0; i < nbSec+1; i++)
        {
            yield return new WaitForSeconds(1);
            IMS.UpdateTimer(nbSec - i);
        }

        gs.isGameOver = true;
        IMS.FinDePartie();
    }

    // Update is called once per frame
    void Update()
    {
        if (gs.isGameOver)
        {
            StopAllCoroutines();
            return;
        }

        if (!gameStarted) return;



        //echap pour mettre le jeu en pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gs.isPaused == false)
            {
                gs.isPaused = true;
                IMS.Pause(gs.isPaused);
            }
            else
            {
                gs.isPaused = false;
                IMS.Pause(gs.isPaused);
            }
        }

        if (gs.isPaused) return;

        IMS.UpdateScore(gs.playerScore1, gs.playerScore2);
        if (gs.playerScore1 >= 3 || gs.playerScore2 >= 3)
        {
            IMS.FinDePartie();
            gs.isGameOver = true;
        }

        PlayerView1.position = gs.playerPosition1;
        PlayerView2.position = gs.playerPosition2;
        frisbeeView.position = gs.frisbeePosition;

        Rules.Step(ref gs, agentJ1.Act(ref gs, Rules.GetAvailableActions1(ref gs)), agentJ2.Act(ref gs, Rules.GetAvailableActions2(ref gs)));
    }
}
