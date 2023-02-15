using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dungeon
{
    public PuzzlePieces[,] DungeonLayout;
    public int CountEmpty
    {
        private set{}
        get
        {
            return GetCountEmpty();
        }
    }
    int GetCountEmpty()
    {
        // this can be done starting in C# 4 but its not working here? "interface co-variance" -> // DungeonLayout.ToList<PuzzlePieces>();
        // see: https://stackoverflow.com/questions/4922129/how-do-i-convert-an-array-to-a-listobject-in-c
        return DungeonLayout.Cast<PuzzlePieces>().ToList().Count(piece => piece == null);
    }
    public Dungeon(int x, int y)
    {
        // should be zero-based by default filling it with the deafult for the object type, which in this case is null
        // see: https://stackoverflow.com/questions/17358139/getupperbound-and-getlowerbound-function-for-array
        DungeonLayout = new PuzzlePieces[x, y];
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
    public PuzzlePieces this[int x]
    {
        get
        {
            return DungeonLayout.Cast<PuzzlePieces>().ToList<PuzzlePieces>()[x];
        }
        set
        {
            DungeonLayout.SetValue(value,x);
        }
    }
}
