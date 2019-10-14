using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

public struct WindjermanGameState
{              
        
    public const float playerRadius = 0.5f;
    public const float playerSpeed = 0.3f;
    public const float frisbeeRadius = 0.5f;
    public const float frisbeeSpeed = 0.5f;
        
        
    //public const Vector2 goalPosition1 = new Vector2(-5,0);

    public Player player1;
    public Player player2;
    public Frisbee Frisbee;
    public float Timer;
    public Vector2 playerPosition1;
    public Vector2 playerPosition2;
    public Vector2 frisbeePosition;
    public bool isGameOver;
    public long lastShootStep;
    public long currentGameStep;
    public int playerScore1;
    public int playerScore2;
    public bool isFreeze1; //Il a le Frisbee
    public bool isFreeze2;
    public bool isStun1; //Touché par une bebom
    public bool isStun2;
    
}