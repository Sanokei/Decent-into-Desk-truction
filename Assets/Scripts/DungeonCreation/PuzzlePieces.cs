using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieces : MonoBehaviour
{
    public int PointsCount
    {
        get
        {
            return Points.Count;
        } 
        private set{}
    }
    public PuzzlePieces(bool valid)
    {
        this.valid = valid;
    }
    public bool valid = true;
    public GameObject Prefab;
    public RoomType RoomType;
    public List<Vector3> Points;
    public List<Direction> Occupied; 
    public List<Direction> Directions;
    public Vector3 this[int index]
    {
        get
        {
            return Points[index];
        }
        private set
        {
            throw new System.InvalidOperationException("Setting this property is not allowed. Dumass mf xd");
        }
    }
}

public enum Direction
{
    North,
    South,
    West,
    East
}