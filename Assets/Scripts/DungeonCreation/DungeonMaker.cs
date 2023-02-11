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

    public List<RoomType> RoomTypes = new List<RoomType>();
    
    DungeonMaker(List<PuzzlePieces> DungeonPieces) // Default to add every piece
    {
        this.DungeonPieces = DungeonPieces;
    }
    /*
        This is some lazy ass injection but it works for now, the best way to do this would be to use ZenJect or another a injection library.
        wont do it for the game jam tho kek
    */
    public DungeonMaker(List<RoomType> RoomType) : this(new List<PuzzlePieces>(UnityEngine.Resources.LoadAll<PuzzlePieces>("Pieces/")))
    {
        this.RoomTypes = RoomType;
    }

    public Dungeon MakeDungeon()
    {
        throw new System.NotImplementedException();
    }

    // internal stuff
    private List<PuzzlePieces> GetDungeonRooms()
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
            int rand = UnityEngine.Random.Range(0,list.Count());
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
    
    private Dungeon OrganizeDungeon(List<PuzzlePieces> AllDungeonRooms)
    {
        Dungeon dungeon = new Dungeon();
        
        // my attempt at a psuedo wave function collapse algo
        
        // first add the first node which is the default
        int nearest = FindLargerNearestSq(RoomTypes.Count());
        PuzzlePieces[,] grid = new PuzzlePieces[nearest,nearest];
        
        

        return dungeon;
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