using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using UnityEngine;

public class DungeonMaker
{
    // injected
    [SerializeField]
    private List<PuzzlePieces> DungeonPieces; /// EVERY prefab for dungeon dont confuse this with the <see cref="Dungeon"/> Class

    // Assume the number of rooms is the count of RoomTypes
        // private int m_NumOfRooms;
    // public int NumOfRooms // Number of Rooms you want
    // {
    //     get
    //     {
    //         return NumOfRooms;
    //     }
    //     set
    //     {
    //         m_NumOfRooms = value;
    //     }
    // }

    // public List<RoomType> RoomTypes = new List<RoomType>();
    
    // No longer important cuz i made it monobehaviour
        // DungeonMaker(List<PuzzlePieces> DungeonPieces) // Default to add every piece
        // {
        //     this.DungeonPieces = DungeonPieces;
        // }
        // /*
        //     This is some lazy ass injection but it works for now, the best way to do this would be to use ZenJect or another a injection library.
        //     wont do it for the game jam tho kek
        // */
        // public DungeonMaker(List<RoomType> RoomType) : this(new List<PuzzlePieces>(UnityEngine.Resources.LoadAll<PuzzlePieces>("Pieces/")))
        // {
        //     this.RoomTypes = RoomType;
        // }

    public Dungeon CreateDungeon(List<RoomType> RoomTypes)
    {
        return OrganizeDungeon(GetDungeonRooms(RoomTypes), RoomTypes);
    }

// internal stuff
    private List<PuzzlePieces> GetDungeonRooms(List<RoomType> RoomTypes)
    {
        
        List<PuzzlePieces> AllDungeonRooms = new List<PuzzlePieces>();

        // Hold all the rooms 
        List<PuzzlePieces> addedRooms = new List<PuzzlePieces>();

        // Add the first piece to the dungeon
        // Assume Index of 0 is the first room you walk into  
        //RroomType default
        AllDungeonRooms.Add(DungeonPieces[0]);

        // Filter every type in the 
        foreach(RoomType roomtype in RoomTypes)
        {
            // for the wanted roomtype get all of them then add a random one 
            
            // cast from Inumerable to List<PuzzlePieces>
            List<PuzzlePieces> list = (List<PuzzlePieces>) from room in DungeonPieces where room.RoomType == roomtype select room;
            int rand = UnityEngine.Random.Range(0,list.Count()); // FIX ME
            addedRooms.Add(list[rand]);
        }
        return AllDungeonRooms;
    }
    int NearestSq(int n)
    {
        return (int) Math.Pow(Math.Round(Math.Sqrt(n)), 2);
    }

    int FindLargerNearestSq(int n)
    {
        int nearest = NearestSq(n);
        while (nearest <= n)
        {
            n += 1;
            nearest = NearestSq(n);
        }
        return nearest;
    }

    int FindLargerNearestOddSq(int n)
    {
        int nearest = FindLargerNearestSq(n);
        return nearest % 2 == 0 ? FindLargerNearestOddSq(n+1) : nearest;
    }
    
    private Dungeon OrganizeDungeon(List<PuzzlePieces> AllDungeonRooms, List<RoomType> RoomTypes)
    {
        // Define room grid
        int nearestSq = FindLargerNearestOddSq(RoomTypes.Count);
        Dungeon dungeon = new Dungeon(nearestSq,nearestSq);

        // Start by adding the first room at the center of the grid + bottom of the grid
        dungeon[nearestSq / 2, 0] = DungeonPieces[0]; // set it to the hub room :flushed:

        // Fill the rest of the grid by collapsing possible rooms
        while (AllDungeonRooms.Count > 0)
        {
            Vector2Int cell = GetLeastEntropyCell(dungeon);
            PuzzlePieces piece = CollapseRoom(cell, AllDungeonRooms, dungeon);

            if (piece == null)
            {
                Debug.LogError("No room found that fits at cell " + cell);
            }

            dungeon[cell.x, cell.y] = piece;
            AllDungeonRooms.Remove(piece);
        }

        return dungeon;
    }

    private Vector2Int GetLeastEntropyCell(Dungeon dungeon)
    {
        int[,] EntropyMap = new int[dungeon.DungeonLayout.GetLength(0),dungeon.DungeonLayout.GetLength(1)];

        for(int row = 0; row < dungeon.DungeonLayout.GetLength(0); row++)
        {
            for(int col = 0; col < dungeon.DungeonLayout.GetLength(1); col++)
            {
                // If the Cell is not empty skip
                if(dungeon[row,col] != null)
                    continue;

                // Get the length of the two array
                int numRows = dungeon.DungeonLayout.GetLength(0);
                int numCols = dungeon.DungeonLayout.GetLength(1);

                // Get the directions
                // FIXME: this is stupid. Just put it in if statments
                PuzzlePieces top = row > 0 ? dungeon[row - 1, col] : new PuzzlePieces(false);
                PuzzlePieces bottom = row < numRows - 1 ? dungeon[row + 1, col] : new PuzzlePieces(false);
                PuzzlePieces left = col > 0 ? dungeon[row, col - 1] : new PuzzlePieces(false);
                PuzzlePieces right = col < numCols - 1 ? dungeon[row, col + 1] : new PuzzlePieces(false);

                // Check the directions and if the opposite is there then add one to entropy
                if(top.valid && top.Directions.Contains(Direction.South) && !top.Occupied.Contains(Direction.South))
                    EntropyMap[row,col]++;
                if(bottom.valid && bottom.Directions.Contains(Direction.North) && !top.Occupied.Contains(Direction.North))
                    EntropyMap[row,col]++;
                if(left.valid && left.Directions.Contains(Direction.East) && !top.Occupied.Contains(Direction.East))
                    EntropyMap[row,col]++;
                if(right.valid && right.Directions.Contains(Direction.West) && !top.Occupied.Contains(Direction.West))
                    EntropyMap[row,col]++;
            }
        }

        // Get the highest number of Entropy
        var maxTuple = EntropyMap.Cast<int>()
            .Select((value, index) => (value, x: index % EntropyMap.GetLength(1), y: index / EntropyMap.GetLength(1)))
            .OrderByDescending(t => t.value)
            .First();

        return new Vector2Int(maxTuple.x,maxTuple.y);
    }

    private PuzzlePieces CollapseRoom(Vector2Int cell, List<PuzzlePieces> allDungeonRooms, Dungeon dungeon)
    {
        // get the best room for the cell provided
        int row = cell.x;
        int col = cell.y;
        int numRows = dungeon.DungeonLayout.GetLength(0);
        int numCols = dungeon.DungeonLayout.GetLength(1);
        int[,] EntropyMap = new int[numRows,numCols];

        PuzzlePieces top = row > 0 ? dungeon[row - 1, col] : new PuzzlePieces(false);
        PuzzlePieces bottom = row < numRows - 1 ? dungeon[row + 1, col] : new PuzzlePieces(false);
        PuzzlePieces left = col > 0 ? dungeon[row, col - 1] : new PuzzlePieces(false);
        PuzzlePieces right = col < numCols - 1 ? dungeon[row, col + 1] : new PuzzlePieces(false);

        foreach(PuzzlePieces piece in allDungeonRooms)
        {
            // Check the directions and if the opposite is there then add one to entropy
            if(top.valid && top.Directions.Contains(Direction.South) && !top.Occupied.Contains(Direction.South) && dungeon[row,col].Directions.Contains(Direction.North) && !dungeon[row,col].Occupied.Contains(Direction.North))
                EntropyMap[row,col]++;
            if(bottom.valid && bottom.Directions.Contains(Direction.North) && !top.Occupied.Contains(Direction.North) && dungeon[row,col].Directions.Contains(Direction.South) && !dungeon[row,col].Occupied.Contains(Direction.South))
                EntropyMap[row,col]++;
            if(left.valid && left.Directions.Contains(Direction.East) && !top.Occupied.Contains(Direction.East) && dungeon[row,col].Directions.Contains(Direction.West) && !dungeon[row,col].Occupied.Contains(Direction.West))
                EntropyMap[row,col]++;
            if(right.valid && right.Directions.Contains(Direction.West) && !top.Occupied.Contains(Direction.West) && dungeon[row,col].Directions.Contains(Direction.East) && !dungeon[row,col].Occupied.Contains(Direction.East))
                EntropyMap[row,col]++;
        }

        var maxTuple = EntropyMap.Cast<int>()
            .Select((value, index) => (value, x: index % EntropyMap.GetLength(1), y: index / EntropyMap.GetLength(1)))
            .OrderByDescending(t => t.value)
            .First();

        return allDungeonRooms[maxTuple.x*maxTuple.y];
    }
}

// see: https://learn.microsoft.com/en-us/dotnet/api/system.enum?view=netcore-2.0
[DataContract]
public enum RoomType
{
    [EnumMember(Value = "MonsterRoom")]
    MonsterRoom,
    [EnumMember(Value = "LootRoom")]
    LootRoom,
    [EnumMember(Value = "ShopRoom")]
    ShopRoom,
    [EnumMember(Value = "RestRoom")]
    RestRoom, // hehe restroom
    [EnumMember(Value = "Unknown")]
    Default
}