using Unity.Entities;
using UnityEngine;

public struct Bomb : IComponentData
{
}

[RequiresEntityConversion]
public class BombAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<Bomb>(entity);
    }
}
