using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

public struct WindjermanGameState
{              
        
    public const float playerRadius = 0.5f;
    public const float playerSpeed = 0.3f;
    public const float frisbeeRadius = 0.5f;
    public const float frisbeeVelocity = 0.05f;
    
        
        
    //public const Vector2 goalPosition1 = new Vector2(-5,0);

    public Vector2 frisbeeSpeed;
    public float Timer;
    public Vector2 playerPosition1;
    public Vector2 playerPosition2;
    public Vector2 frisbeePosition;
    public bool frisbeeFrozen;
    public bool isGameOver;
    public bool isPaused;
    public int playerScore1;
    public int playerScore2;
    public bool isFreeze1; //Il a le Frisbee
    public bool isFreeze2;
    public bool isStun1; //Touché par une bebom
    public bool isStun2;
    public IAgent agentJ1;
    public IAgent agentJ2;
    
}