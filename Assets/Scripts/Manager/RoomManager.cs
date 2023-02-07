using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<RoomScriptableObject> rooms;
    public Room roomPrefab;

    private void Start()
    {
        // Generate level
        WaveFunctionCollapse wfc = new WaveFunctionCollapse();
        wfc.rooms = rooms;
        wfc.GenerateLevel();

        // Instantiate rooms
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (wfc.levelMap[i, j] != 0)
                {
                    // Get room
                    RoomScriptableObject room = rooms[wfc.levelMap[i, j] - 1];

                    // Instantiate room prefab with room generator
                    Room roomObject = Instantiate(roomPrefab, new Vector3(i * 10, 0, j * 10), Quaternion.identity);
                    roomObject.room = room;
                    roomObject.GenerateRoom();
                }
            }
        }
    }
}