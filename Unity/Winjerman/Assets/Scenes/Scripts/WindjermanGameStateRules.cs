using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;


public static class WindjermanGameStateRules
{
       
       public static void Init(ref WindjermanGameState gs)
       {
              gs.Timer = 0f;
              gs.isFreeze1 = true;
              gs.isFreeze2 = false;
              gs.frisbeeFrozen = false;
              gs.isStun1 = false;
              gs.isStun2 = false;
              gs.frisbeePosition = new Vector2(-5f, 0);
              gs.playerScore1 = 0;
              gs.playerScore2 = 0;
              gs.playerPosition1 = new Vector2(-5f,0f);
              gs.playerPosition2 = new Vector2(5f,0f);
              gs.isGameOver = false;
              gs.dureePartie = 120;
              
       }
       
       public static void Step(ref WindjermanGameState gs, int chosenPlayerAction1, int chosenPlayerAction2)
       {
              if (gs.isGameOver)
              {
                    //throw new Exception("YOU SHOULD NOT TRY TO UPDATE GAME STATE WHEN GAME IS OVER !!!");
              }
              UpdateFrisbee(ref gs);
              HandleAgentInputs1(ref gs, chosenPlayerAction1);
              HandleAgentInputs2(ref gs, chosenPlayerAction2);
              HandleCollisions(ref gs);
       }


       public static void UpdateFrisbee(ref WindjermanGameState gs)
       {
              
              if (gs.frisbeeFrozen)
              {
                     gs.frisbeeSpeed = new Vector2(0,0);
                     return;
              }
              gs.frisbeePosition += gs.frisbeeSpeed;
              
       }

       public static void HandleCollisions(ref WindjermanGameState gs)
       {
              var sqrDistance1 = (gs.frisbeePosition - gs.playerPosition1).sqrMagnitude;
              var sqrDistance2 = (gs.frisbeePosition - gs.playerPosition2).sqrMagnitude;
              
              
              //Collision with Players
              if ((sqrDistance1
                    <= Mathf.Pow( WindjermanGameState.frisbeeRadius+ WindjermanGameState.playerRadius,
                           2)))
              {
                     gs.frisbeePosition = new Vector2(gs.playerPosition1.x+1.2f, gs.playerPosition1.y);
                     gs.isFreeze1 = true;
                     gs.frisbeeFrozen = true;
                     

              }
              else if ((sqrDistance2
                        <= Mathf.Pow( WindjermanGameState.frisbeeRadius+ WindjermanGameState.playerRadius,
                               2)))
              {
                     gs.frisbeePosition = new Vector2(gs.playerPosition2.x-1.2f, gs.playerPosition2.y);
                     gs.isFreeze2 = true;
                     gs.frisbeeFrozen = true;
              }

              //Rebound
              if (gs.frisbeePosition.y > 5 || gs.frisbeePosition.y < -5)
              {
                     gs.frisbeeSpeed = new Vector2(gs.frisbeeSpeed.x, -gs.frisbeeSpeed.y);
              }
              
              //GOAL !
              if (gs.frisbeePosition.x > 10)
              {
                     gs.playerPosition1 = new Vector2(-5f,0f);
                     gs.playerPosition2 = new Vector2(5f,0f);
                     gs.frisbeeSpeed = new Vector2(0,0);
                     gs.frisbeePosition = new Vector2(5f, 0);
                     gs.playerScore1 += 1;

              }

              if (gs.frisbeePosition.x < -10)
              {
                     gs.playerPosition1 = new Vector2(-5f,0f);
                     gs.playerPosition2 = new Vector2(5f,0f);
                     gs.frisbeeSpeed = new Vector2(0,0);
                     gs.frisbeePosition = new Vector2(-5f, 0);
                     gs.playerScore2 += 1;
              }

              
       }


       static void HandleAgentInputs1(ref WindjermanGameState gs, int chosenPlayerAction1)
       {
              var frisbeeVelocity = WindjermanGameState.frisbeeVelocity;
              switch (chosenPlayerAction1)
              {
                     case 0: // DO NOTHING
                            break;
                     case 1: // LEFT
                     {
                            if (!gs.isFreeze1 && gs.playerPosition1.x > -9f)
                            {
                                   gs.playerPosition1 += Vector2.left * WindjermanGameState.playerSpeed;      
                                   
                            }
                            break;
                     }
                     case 2: // RIGHT
                     {
                            if (!gs.isFreeze1 && gs.playerPosition1.x < -2f)
                            {
                                   gs.playerPosition1 += Vector2.right * WindjermanGameState.playerSpeed;
                            }
                            break;
                     }
                     case 3: // UP
                     {
                            if (!gs.isFreeze1)
                            {
                                   if (!(gs.playerPosition1.y > 5))
                                   {
                                          gs.playerPosition1 += Vector2.up * WindjermanGameState.playerSpeed;       
                                   }
                                                                      
                            }
                            break;
                     }
                     case 4: // DOWN
                     {
                            if (!gs.isFreeze1)
                            {
                                   if (!(gs.playerPosition1.y < -5))
                                   {
                                          gs.playerPosition1 += Vector2.down * WindjermanGameState.playerSpeed;
                                   }
                                                                      
                            }
                            break;
                     }
                     case 5: //SHOOT DOWN
                     {
                            if (gs.isFreeze1)
                            {
//                                   Debug.Log(gs.frisbeeFrozen);
                                   gs.frisbeeFrozen = false;
                                   gs.frisbeeSpeed = new Vector2(frisbeeVelocity, -frisbeeVelocity);
                                   gs.isFreeze1 = false;
                            }
                            break;
                     }
                     case 6: // SHOOT STRAIGHT
                     {
                            if (gs.isFreeze1)
                            {
                                   gs.frisbeeFrozen = false;
                                   gs.frisbeeSpeed = new Vector2(frisbeeVelocity, 0f);
                                   gs.isFreeze1 = false;
                            }
                            break;
                     }
                     case 7: // SHOOT UP
                     {
                            if (gs.isFreeze1)
                            {
//                                   Debug.Log(gs.frisbeeFrozen);
                                   gs.frisbeeFrozen = false;
                                   gs.frisbeeSpeed = new Vector2(frisbeeVelocity, frisbeeVelocity);
                                   gs.isFreeze1 = false;
                            }
                            break;
                     }
              }
       }
       
       
       static void HandleAgentInputs2(ref WindjermanGameState gs, int chosenPlayerAction2)
       {
              var frisbeeVelocity = WindjermanGameState.frisbeeVelocity;
              switch (chosenPlayerAction2)
              {
                     case 0: // DO NOTHING
                            break;
                     case 1: // LEFT
                     {
                            if (gs.isFreeze2)
                            {
                                   break;
                            }

                            if (gs.playerPosition2.x > 2f)
                            {
                                   gs.playerPosition2 += Vector2.left * WindjermanGameState.playerSpeed;
                            }

                            break;
                     }
                     case 2: // RIGHT
                     {
                            if (gs.isFreeze2)
                            {
                                   break;
                            }
                            if (gs.playerPosition2.x < 9f)
                            {
                                   gs.playerPosition2 += Vector2.right * WindjermanGameState.playerSpeed;
                            }

                            break;
                     }
                     case 3: // UP
                     {
                            if (gs.isFreeze2)
                            {
                                   break;
                            }
                            if (!(gs.playerPosition2.y > 5f))
                            {
                                   gs.playerPosition2 += Vector2.up * WindjermanGameState.playerSpeed;       
                            }
                            break;
                            
                     }
                     case 4: // DOWN
                     {
                            if (gs.isFreeze2)
                            {
                                   break;
                            }
                            if (!(gs.playerPosition2.y < -5))
                            {
                                   gs.playerPosition2 += Vector2.down * WindjermanGameState.playerSpeed;       
                            }
                            break;
                     }
                     case 5: //SHOOT DOWN
                     {
                            
                            if (gs.isFreeze2)
                            {
                                   gs.frisbeeFrozen = false;
                                   gs.frisbeeSpeed = new Vector2(-frisbeeVelocity, -frisbeeVelocity);
                                   gs.isFreeze2 = false;
                            }
                            break;
                     }
                     case 6: // SHOOT STRAIGHT
                     {
                            if (gs.isFreeze2)
                            {
                                   gs.frisbeeFrozen = false;
                                   gs.frisbeeSpeed = new Vector2(-frisbeeVelocity, 0f);
                                   gs.isFreeze2 = false;
                            }
                            break;
                     }
                     case 7: // SHOOT UP
                     {
                            if (gs.isFreeze2)
                            {
                                   gs.frisbeeFrozen = false;
                                   gs.frisbeeSpeed = new Vector2(-frisbeeVelocity, frisbeeVelocity);
                                   gs.isFreeze2 = false;
                            }
                            break;
                     }
              }
       }
       
       public static readonly NativeList<int> AvailableActionsFree = new NativeList<int>(8, Allocator.Persistent)
       {
              0, 1, 2, 3, 4, 5, 6, 7
       };
       
       public static readonly NativeList<int> AvailableActionsFrozen = new NativeList<int>(4, Allocator.Persistent)
       {
              0, 5, 6, 7
       };
       
       public static readonly NativeList<int> AvailableActionsStun = new NativeList<int>(1, Allocator.Persistent)
       {
              0
       };
       

       public static NativeList<int> GetAvailableActions1(ref WindjermanGameState gs)
       {
              if (gs.isFreeze1)
              {
                     return AvailableActionsFrozen;
              }
              if (gs.isStun1)
              {
                     return AvailableActionsStun;
              }

              return AvailableActionsFree;

       }
       
       public static NativeList<int> GetAvailableActions2(ref WindjermanGameState gs)
       {
              if (gs.isFreeze2)
              {
                     return AvailableActionsFrozen;
              }
              if (gs.isStun2)
              {
                     return AvailableActionsStun;
              }

              return AvailableActionsFree;
       }
       
       public static int GetAvailableActions1int(ref WindjermanGameState gs)
       {
              if (gs.isFreeze2)
              {
                     return 1;
              }
              if (gs.isStun2)
              {
                     return 2;
              }

              return 0;
       }
       
       public static int GetAvailableActions2int(ref WindjermanGameState gs)
       {
              if (gs.isFreeze2)
              {
                     return 1;
              }
              if (gs.isStun2)
              {
                     return 2;
              }

              return 0;
       }
       

       public static WindjermanGameState Clone(ref WindjermanGameState gs)
       {
              WindjermanGameState gsCopy = new WindjermanGameState();
              gsCopy.frisbeeSpeed = gs.frisbeeSpeed;
              gsCopy.Timer = gs.Timer;
              gsCopy.playerPosition1 = gs.playerPosition1;
              gsCopy.playerPosition2 = gs.playerPosition2;
              gsCopy.frisbeePosition = gs.frisbeePosition;
              gsCopy.frisbeeFrozen = gs.frisbeeFrozen;
              gsCopy.isGameOver = gs.isGameOver;
              gsCopy.playerScore1 = gs.playerScore1;
              gsCopy.playerScore2 = gs.playerScore2;
              gsCopy.isFreeze1 = gs.isFreeze1;
              gsCopy.isFreeze2 = gs.isFreeze2;
              gsCopy.isStun1 = gs.isStun1;
              gsCopy.isStun2 = gs.isStun2;
              

              return gsCopy;
       }

       public static long GetHashCodeJ1(ref WindjermanGameState gs)
       {
              var hash = 0L;
              hash += 10 + (long) math.round(math.clamp(math.distance(gs.playerPosition1.x, gs.frisbeePosition.x), 0, 8));
              hash += 10 + (long) math.round(math.clamp(math.distance(gs.playerPosition1.y, gs.frisbeePosition.y), 0, 10));
              return hash;
       }
       
       public static long GetHashCodeJ2(ref WindjermanGameState gs)
       {
              var hash = 0L;
              hash += 10 + (long) math.round(math.clamp(math.distance(gs.playerPosition2.x, gs.frisbeePosition.x), 0, 8));
              hash += 10 + (long) math.round(math.clamp(math.distance(gs.playerPosition2.y, gs.frisbeePosition.y), 0, 10));
              return hash;
       }
}
