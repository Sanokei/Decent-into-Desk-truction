using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomScriptableObject room;
    public GameObject wallPrefab,doorPrefab;

    public void GenerateRoom()
    {
        // Instantiate walls
        if (room.north == Direction.Wall)
        {
            Instantiate(wallPrefab, new Vector3(0, 0, 5), Quaternion.identity);
        }
        if (room.south == Direction.Wall)
        {
            Instantiate(wallPrefab, new Vector3(0, 0, -5), Quaternion.identity);
        }
        if (room.east == Direction.Wall)
        {
            Instantiate(wallPrefab, new Vector3(5, 0, 0), Quaternion.identity);
        }
        if (room.west == Direction.Wall)
        {
            Instantiate(wallPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        }
        
        if (room.north == Direction.DoorWay)
        {
            Instantiate(doorPrefab, new Vector3(0, 0, 5), Quaternion.identity);
        }
        if (room.south == Direction.DoorWay)
        {
            Instantiate(doorPrefab, new Vector3(0, 0, -5), Quaternion.identity);
        }
        if (room.east == Direction.DoorWay)
        {
            Instantiate(doorPrefab, new Vector3(5, 0, 0), Quaternion.identity);
        }
        if (room.west == Direction.DoorWay)
        {
            Instantiate(doorPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        }
    }
}