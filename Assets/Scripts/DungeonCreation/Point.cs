using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public List<Vector3> Points;
    public Direction Direction;
    [HideInInspector] public bool[,] Occupied; 
}

public enum Direction
{
    North,
    South,
    West,
    East
}