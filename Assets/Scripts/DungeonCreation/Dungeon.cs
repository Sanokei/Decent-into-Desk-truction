using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    private List<PuzzlePieces> DungeonPieces;

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

    public Dungeon Add(PuzzlePieces item)
    {
        DungeonPieces.Add(item);
        return this;
    }
}

