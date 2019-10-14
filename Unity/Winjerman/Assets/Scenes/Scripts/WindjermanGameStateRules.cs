using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public static class WindjermanGameStateRules
{
       public static void Init(ref WindjermanGameState gs)
       {
              gs.Timer = 0f;
              gs.isFreeze1 = true;
              gs.isFreeze2 = false;
              gs.isStun1 = false;
              gs.isStun2 = false;
              gs.frisbeePosition = new Vector2(-4f, 0);
              gs.playerScore1 = 0;
              gs.playerScore2 = 0;
              gs.playerPosition1 = new Vector2(-5f,0f);
              gs.playerPosition2 = new Vector2(5f,0f);
              gs.isGameOver = false;
              gs.Frisbee = new Frisbee()
              {
                     position = new Vector2(-4f, 0),
                     speed = new Vector2(0f,0f)
              };
              gs.player1 = new Player()
              {
                     isFreeze = true,
                     isStun = false,
                     position = new Vector2(-5f, 0f),
                     speed = new Vector2(0f,0f)
              };
              gs.player2 = new Player()
              {
                     isFreeze = false,
                     isStun = false,
                     position = new Vector2(5f, 0f),
                     speed = new Vector2(0f,0f)
              };
       }
       
       public static void Step(ref WindjermanGameState gs, int chosenPlayerAction)
       {
              if (gs.isGameOver)
              {
                     throw new Exception("YOU SHOULD NOT TRY TO UPDATE GAME STATE WHEN GAME IS OVER !!!");
              }
              UpdateFrisbee(ref gs);
              //HandleAgentInputs(ref gs, chosenPlayerAction);
              HandleCollisions(ref gs);
              gs.currentGameStep += 1;
       }


       public static void UpdateFrisbee(ref WindjermanGameState gs)
       {
              gs.Frisbee.position += gs.Frisbee.speed;
       }

       public static void HandleCollisions(ref WindjermanGameState gs)
       {
              var sqrDistance1 = (gs.Frisbee.position - gs.playerPosition1).sqrMagnitude;
              var sqrDistance2 = (gs.Frisbee.position - gs.playerPosition2).sqrMagnitude;
              
              if ((sqrDistance1
                    <= Mathf.Pow( WindjermanGameState.frisbeeRadius+ WindjermanGameState.playerRadius,
                           2)))
              {
                     gs.isFreeze1 = true;
              }
              else if ((sqrDistance2
                        <= Mathf.Pow( WindjermanGameState.frisbeeRadius+ WindjermanGameState.playerRadius,
                               2)))
              {
                     gs.isFreeze2 = true;
              }
              //TODO: Cage à faire !! 
              
       }


       static void HandleAgentInputs(ref WindjermanGameState gs, int chosenPlayerAction, int playerId)
       {
              if (playerId == 1)
              {
                     switch (chosenPlayerAction)
                     {
                            case 0: // DO NOTHING
                                   return;
                            case 1: // LEFT
                            {
                                   gs.playerPosition1 += Vector2.left * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 2: // RIGHT
                            {
                                   gs.playerPosition1 += Vector2.right * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 3: // UP
                            {
                                   gs.playerPosition1 += Vector2.up * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 4: // DOWN
                            {
                                   gs.playerPosition1 += Vector2.down * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 5: // UP LEFT
                            {
                                   gs.playerPosition1 += Vector2.left * Vector2.up * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 6: // UP RIGHT
                            {
                                   gs.playerPosition1 += Vector2.right * Vector2.up * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 7: // DOWN LEFT
                            {
                                   gs.playerPosition1 += Vector2.left * Vector2.down * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 8: // DOWN RIGHT
                            {
                                   gs.playerPosition1 += Vector2.right * Vector2.down * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 9: //SHOOT DOWN
                            {
                                   break;
                            }
                            case 10: // SHOOT STRAIGHT
                            {
                                   break;
                            }
                            case 11: // SHOOT UP
                            {
                                   break;
                            }
                     }
              }
              else if (playerId == 2)
              {
                     switch (chosenPlayerAction)
                     {
                            case 0: // DO NOTHING
                                   return;
                            case 1: // LEFT
                            {
                                   gs.playerPosition2 += Vector2.left * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 2: // RIGHT
                            {
                                   gs.playerPosition2 += Vector2.right * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 3: // UP
                            {
                                   gs.playerPosition2 += Vector2.up * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 4: // DOWN
                            {
                                   gs.playerPosition2 += Vector2.down * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 5: // UP LEFT
                            {
                                   gs.playerPosition2 += Vector2.left * Vector2.up * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 6: // UP RIGHT
                            {
                                   gs.playerPosition2 += Vector2.right * Vector2.up * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 7: // DOWN LEFT
                            {
                                   gs.playerPosition2 += Vector2.left * Vector2.down * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 8: // DOWN RIGHT
                            {
                                   gs.playerPosition2 += Vector2.right * Vector2.down * WindjermanGameState.playerSpeed;
                                   break;
                            }
                            case 9: //SHOOT DOWN
                            {
                                   break;
                            }
                            case 10: // SHOOT STRAIGHT
                            {
                                   break;
                            }
                            case 11: // SHOOT UP
                            {
                                   break;
                            }
                     }

              }
       }
}
