﻿using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Rules = WindjermanGameStateRules;
using Spawn = SpawnSystem;
using Unity.Entities;
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
        

        //création des agents en fonction des choix effectués sur l'interface
        switch (choix1)
        {
            case listeChoix.HUMAN:

                agentJ1 = new HumanPlayerAgent(0);
                break;

            case listeChoix.RANDOM:

                agentJ1 = new RandomAgent
                {
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
        gs.agentJ1 = agentJ1;
        gs.agentJ2 = agentJ2;
        
        World.Active.GetExistingSystem<SpawnSystem>().StartGame(agentJ1,agentJ2);
        
        

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
        /*if (Input.GetKeyDown(KeyCode.Escape))
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
        }*/

        if (gs.isPaused) return;
        
        Debug.Log(gs.playerScore1 + " - " + gs.playerScore2);
        
        gs.playerScore1 = World.Active.GetExistingSystem<SpawnSystem>().score1();
        gs.playerScore2 = World.Active.GetExistingSystem<SpawnSystem>().score2();

        IMS.UpdateScore(gs.playerScore1, gs.playerScore2);
        if (gs.playerScore1 >= 3 || gs.playerScore2 >= 3)
        {
             Debug.Log("un des joueurs aatteint le score max");
            
            IMS.FinDePartie();
            gs.isGameOver = true;
        }
        
        World.Active.GetExistingSystem<SpawnSystem>().Update();
       
    }
}
