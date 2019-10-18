using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;
using Rules = WindjermanGameStateRules;


public class SpawnSystem : ComponentSystem
{
    
    private WindjermanGameState gs;
    public Entity PlayerView1;
    public Entity PlayerView2;
    public Entity frisbeeView;
    
    private IAgent agentJ1;
    private IAgent agentJ2;
    
    bool gameStarted = false;

    public void StartGame(IAgent choix1, IAgent choix2)
    {
        //Initialisation des règles
        Rules.Init(ref gs);
        
        var spawners = Entities.WithAll<Spawner>().ToEntityQuery().ToComponentDataArray<Spawner>(Allocator.TempJob);
        
        //instanciation des éléments graphiques
        PlayerView1 = EntityManager.Instantiate(spawners[0].player1Prefab);
            
        EntityManager.SetComponentData(PlayerView1, new Translation
        {
            Value = new float3(-5f, 0f, 0f)
        });
        
        PlayerView2 = EntityManager.Instantiate(spawners[0].player2Prefab);
            
        EntityManager.SetComponentData(PlayerView2, new Translation
        {
            Value = new float3(5f, 0f, 0f)
        });
        
        frisbeeView = EntityManager.Instantiate(spawners[0].frisbeePrefab);
            
        EntityManager.SetComponentData(frisbeeView, new Translation
        {
            Value = new float3(-5f, 0f, 0f)
        });
        

        //création des agents en fonction des choix effectués sur l'interface

        agentJ1 = choix1;
        agentJ2 = choix2;

        gameStarted = true;
        
        Debug.Log("startgame du spawnsystem");
    }

    protected override void OnUpdate()
    {
        
        
        
        
        
    }

    public void Update()
    {
        EntityManager.SetComponentData(PlayerView1, new Translation
        {
            Value = new float3(gs.playerPosition1.x, gs.playerPosition1.y, 0f)
        });
        
        EntityManager.SetComponentData(PlayerView2, new Translation
        {
            Value = new float3(gs.playerPosition2.x, gs.playerPosition2.y, 0f)
        });
        
        EntityManager.SetComponentData(frisbeeView, new Translation
        {
            Value = new float3(gs.frisbeePosition.x, gs.frisbeePosition.y, 0f)
        });
        

        Rules.Step(ref gs, agentJ1.Act(ref gs, Rules.GetAvailableActions1(ref gs)), agentJ2.Act(ref gs, Rules.GetAvailableActions2(ref gs)));
        
        Debug.Log("l'update se fait");
 
        
    }

    public int score1()
    {
        return gs.playerScore1;
    }
    
    public int score2()
    {
        return gs.playerScore2;
    }

}