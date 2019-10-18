using Unity.Entities;
using UnityEngine;

public struct Player1 : IComponentData
{
}

[RequiresEntityConversion]
public class Player1Authoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<Player1>(entity);
    }
}