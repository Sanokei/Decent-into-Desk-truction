using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "RoomScriptableObject")]
public class RoomScriptableObject : ScriptableObject
{
    // Room properties, rules, and behaviors
    public GameObject prefab;
    public RoomRule rule;
    public Direction north, south, east, west;
}

public enum RoomRule
{
    AllowMonsters,
    DisallowMonsters,
    SpecialRoom
}
public enum Direction
{
    DoorWay,
    Wall
}