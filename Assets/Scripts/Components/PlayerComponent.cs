using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct Player: IComponentData
{
    public float health;
    public Vector2 direction;
}
