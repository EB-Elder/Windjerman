﻿using Unity.Entities;
using UnityEngine;

public struct Player2 : IComponentData
{
}

[RequiresEntityConversion]
public class Player2Authoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<Player2>(entity);
    }
}