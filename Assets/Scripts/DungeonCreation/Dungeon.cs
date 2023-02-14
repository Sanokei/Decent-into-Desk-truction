using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    List<PuzzlePieces> DungeonPieces = new List<PuzzlePieces>();
    PuzzlePieces[,] DungeonLayout;

    public PuzzlePieces this[int index]
    {
        get
        {
            return DungeonPieces[index];
        }
        set
        {
            DungeonPieces[index] = value;
        }
    }
    
    public PuzzlePieces this[int x, int y]
    {
        get
        {
            return DungeonLayout[x, y];
        }
        set
        {
            DungeonLayout[x, y] = value;
        }
    }

    public Dungeon Add(PuzzlePieces item)
    {
        DungeonPieces.Add(item);
        return this;
    }
}
