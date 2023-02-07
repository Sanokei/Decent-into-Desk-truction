using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    /*
        See: https://github.com/mxgmn/WaveFunctionCollapse
    */
public class WaveFunctionCollapse
{
    public List<RoomScriptableObject> rooms; // injected at RoomManager Start
    public int[,] levelMap;

    public void GenerateLevel()
    {
        // Initialize level map
        levelMap = new int[10, 10];

        // Initialize first room
        int x = Random.Range(0, 10);
        int y = Random.Range(0, 10);
        levelMap[x, y] = 1;

        // Generate rooms
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (levelMap[i, j] == 1)
                {
                    // Get all possible rooms
                    List<RoomScriptableObject> possibleRooms = GetPossibleRooms(i, j);

                    // Choose a random room
                    int randomRoom = Random.Range(0, possibleRooms.Count);
                    RoomScriptableObject room = possibleRooms[randomRoom];

                    // Place room
                    levelMap[i, j] = 1;
                }
            }
        }
    }

    private List<RoomScriptableObject> GetPossibleRooms(int x, int y)
    {
        List<RoomScriptableObject> possibleRooms = new List<RoomScriptableObject>();

        foreach (RoomScriptableObject room in rooms)
        {
            // Check if room is valid
            if (IsValidRoom(x, y, room))
            {
                possibleRooms.Add(room);
            }
        }

        return possibleRooms;
    }

    private bool IsValidRoom(int x, int y, RoomScriptableObject room)
    {
        // Check if room is within bounds
        if (x < 0 || x > 9 || y < 0 || y > 9)
        {
            return false;
        }

        // Check if room is empty
        if (levelMap[x, y] != 0)
        {
            return false;
        }

        // add more rules later

        return true;
    }
}