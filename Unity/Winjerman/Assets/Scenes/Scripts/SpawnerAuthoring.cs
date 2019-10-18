using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Spawner : IComponentData
{
    public Entity frisbeePrefab;
    public Entity player1Prefab;
    public Entity player2Prefab;
    public Entity bombPrefab;
}

public class SpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject frisbeePrefab;

    public GameObject player1Prefab;
    
    public GameObject player2Prefab;

    public GameObject bombPrefab;


    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawner = new Spawner
        {
            player1Prefab = conversionSystem.GetPrimaryEntity(player1Prefab),
            player2Prefab = conversionSystem.GetPrimaryEntity(player2Prefab),
            frisbeePrefab = conversionSystem.GetPrimaryEntity(frisbeePrefab),
            bombPrefab = conversionSystem.GetPrimaryEntity(bombPrefab),
        };
        
        dstManager.AddComponent<Spawner>(entity);
        dstManager.SetComponentData(entity, spawner);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(frisbeePrefab);
        referencedPrefabs.Add(player1Prefab);
        referencedPrefabs.Add(player2Prefab);
        referencedPrefabs.Add(bombPrefab);
    }
}