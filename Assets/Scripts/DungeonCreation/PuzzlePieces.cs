using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieces : MonoBehaviour
{
    public int PointsCount{get; private set;}
    public GameObject Prefab;
    public RoomType RoomType;
    public List<Point> Points = new List<Point>();

    public Point this[int index]
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
    void OnEnable()
    {
        PointsCount = Points.Count;
    }
}
