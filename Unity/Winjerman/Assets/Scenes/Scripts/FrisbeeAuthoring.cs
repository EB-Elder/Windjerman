using Unity.Entities;
using UnityEngine;

public struct Frisbee : IComponentData
{
}

[RequiresEntityConversion]
public class FrisbeeAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<Frisbee>(entity);
    }
}