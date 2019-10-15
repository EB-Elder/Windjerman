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
              
              
       }
       
       public static void Step(ref WindjermanGameState gs, int chosenPlayerAction1, int chosenPlayerAction2)
       {
              if (gs.isGameOver)
              {
                     throw new Exception("YOU SHOULD NOT TRY TO UPDATE GAME STATE WHEN GAME IS OVER !!!");
              }
              UpdateFrisbee(ref gs);
              HandleAgentInputs1(ref gs, chosenPlayerAction1);
              HandleAgentInputs2(ref gs, chosenPlayerAction2);
              HandleCollisions(ref gs);
              gs.currentGameStep += 1;
       }


       public static void UpdateFrisbee(ref WindjermanGameState gs)
       {
              gs.frisbeePosition += gs.frisbeeSpeed;
       }

       public static void HandleCollisions(ref WindjermanGameState gs)
       {
              var sqrDistance1 = (gs.frisbeePosition - gs.playerPosition1).sqrMagnitude;
              var sqrDistance2 = (gs.frisbeePosition - gs.playerPosition2).sqrMagnitude;
              
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


       static void HandleAgentInputs1(ref WindjermanGameState gs, int chosenPlayerAction1)
       {
              switch (chosenPlayerAction1)
              {
                     case 0: // DO NOTHING
                            return;
                     case 1: // LEFT
                     {
                            if (!gs.isFreeze1)
                            {
                                   gs.playerPosition1 += Vector2.left * WindjermanGameState.playerSpeed;                                   
                            }
                            break;
                     }
                     case 2: // RIGHT
                     {
                            if (!gs.isFreeze1)
                            {
                                   gs.playerPosition1 += Vector2.right * WindjermanGameState.playerSpeed;
                            }
                            break;
                     }
                     case 3: // UP
                     {
                            if (!gs.isFreeze1)
                            {
                                   gs.playerPosition1 += Vector2.up * WindjermanGameState.playerSpeed;                                   
                            }
                            break;
                     }
                     case 4: // DOWN
                     {
                            if (!gs.isFreeze1)
                            {
                                   gs.playerPosition1 += Vector2.down * WindjermanGameState.playerSpeed;                                   
                            }
                            break;
                     }
                     case 5: // UP LEFT
                     {
                            if (!gs.isFreeze1)
                            {
                                   gs.playerPosition1 += Vector2.left * Vector2.up * WindjermanGameState.playerSpeed;                                   
                            }
                            break;
                     }
                     case 6: // UP RIGHT
                     {
                            if (!gs.isFreeze1)
                            {      
                                   gs.playerPosition1 += Vector2.right * Vector2.up * WindjermanGameState.playerSpeed;                                   
                            }
                            break;
                     }
                     case 7: // DOWN LEFT
                     {
                            if (!gs.isFreeze1)
                            {
                                   gs.playerPosition1 += Vector2.left * Vector2.down * WindjermanGameState.playerSpeed;                                   
                            }
                            break;
                     }
                     case 8: // DOWN RIGHT
                     {
                            if (!gs.isFreeze1)
                            {
                                   gs.playerPosition1 += Vector2.right * Vector2.down * WindjermanGameState.playerSpeed;                                   
                            }
                            break;
                     }
                     case 9: //SHOOT DOWN
                     {
                            if (gs.isFreeze1)
                            {
                                   gs.frisbeeSpeed = new Vector2(1f, -1f);
                            }
                            break;
                     }
                     case 10: // SHOOT STRAIGHT
                     {
                            if (gs.isFreeze1)
                            {
                                   gs.frisbeeSpeed = new Vector2(1f, 0f);
                            }
                            break;
                     }
                     case 11: // SHOOT UP
                     {
                            if (gs.isFreeze1)
                            {
                                   gs.frisbeeSpeed = new Vector2(1f, 1f);
                            }
                            break;
                     }
              }
       }
       
       
       static void HandleAgentInputs2(ref WindjermanGameState gs, int chosenPlayerAction2)
       {
              switch (chosenPlayerAction2)
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
                            if (gs.isFreeze2)
                            {
                                   gs.frisbeeSpeed = new Vector2(-1f, -1f);
                            }
                            break;
                     }
                     case 10: // SHOOT STRAIGHT
                     {
                            if (gs.isFreeze2)
                            {
                                   gs.frisbeeSpeed = new Vector2(-1f, 0f);
                            }
                            break;
                     }
                     case 11: // SHOOT UP
                     {
                            if (gs.isFreeze2)
                            {
                                   gs.frisbeeSpeed = new Vector2(-1f, 1f);
                            }
                            break;
                     }
              }
       }
       
       private static readonly int[] AvailableActions = new[]
       {
              0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
       };

       public static int[] GetAvailableActions(ref WindjermanGameState gs)
       {
              return AvailableActions;
       }
}
