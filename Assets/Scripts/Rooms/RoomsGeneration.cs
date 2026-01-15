using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RoomsGeneration : MonoBehaviour
{
    public const int MAP_HEIGHT = 20;
    public const int MAP_WIDTH = 20;
    [SerializeField] public bool[] availableNodes;

    private List<Vector2> Directions = new List<Vector2> { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    [System.Serializable]
    public class Rooms
    {
        public Vector2 GridPos;
        public int freeWalls = 3;
        public int[] roomLayout = new int[4]; // -1 is invalid, 0 is nothing, 1 is entry, 2 is exit // from up to left
    }
    [SerializeField] public List<Rooms> allAvailableRooms = new List<Rooms>();
    public List<int> unblockedRooms = new List<int>();

    public Vector2 START_POINT = Vector2.zero;
    public int MAX_ROOMS = 15;
    public int WALL_SPLIT_RNG = 2;

    public GameObject DOT;

    void Start()
    {
        availableNodes = new bool[MAP_HEIGHT * MAP_WIDTH];
        Vector2 lastRoom = Vector2.zero;
        Vector2 currentPosition = START_POINT;

        bool jumpedOnRoom = false;
        Rooms duplicateRoom = new Rooms();

        for (int i = 0; i < MAX_ROOMS; i++)
        {
            Rooms currentRoom = new Rooms();
            if (!jumpedOnRoom)
            {
                currentRoom.GridPos = currentPosition;
                CheckNode(currentPosition);
            }
            else
            {
                currentRoom = duplicateRoom;
            }

            if (i == 0)
                currentRoom.roomLayout[2] = 1;
            else
                currentRoom.roomLayout[Directions.IndexOf((currentPosition - lastRoom) * -1)] = 1;

            lastRoom = currentPosition;
            int availableExit = 0;
            int RNGwallSplit = Random.Range(0, WALL_SPLIT_RNG);
            if (RNGwallSplit == 0 && i > 1)
                availableExit = -1;
            else
                availableExit = AddExitToRoom(currentRoom);
            if (availableExit == -1)
            {
                allAvailableRooms.Add(currentRoom);
                unblockedRooms.Add(allAvailableRooms.IndexOf(currentRoom));
                int splitRoomIndex = unblockedRooms[Random.Range(1, unblockedRooms.Count)];
                bool hasNoOptions = false;
                int newWall = AddExitToRoom(allAvailableRooms[splitRoomIndex]);
                while (newWall == -1)
                {
                    unblockedRooms.Remove(splitRoomIndex);
                    if (unblockedRooms.Count <= 1)
                    {
                        hasNoOptions = true;
                        break;
                    }
                    splitRoomIndex = unblockedRooms[Random.Range(1, unblockedRooms.Count)];
                    newWall = AddExitToRoom(allAvailableRooms[splitRoomIndex]);
                }
                if (!hasNoOptions)
                {
                    currentPosition = allAvailableRooms[splitRoomIndex].GridPos + Directions[newWall];
                    lastRoom = allAvailableRooms[splitRoomIndex].GridPos;
                    continue;
                }
                break;
            } 
            currentPosition += Directions[availableExit];
            allAvailableRooms.Add(currentRoom);
            unblockedRooms.Add(allAvailableRooms.IndexOf(currentRoom));
            jumpedOnRoom = false;
        }
        AddBossRoom(allAvailableRooms);
        DrawDots();
    }

    public void CheckNode(Vector2 pos)
    {
        //Debug.Log(pos);
        int index = (int)pos.y * MAP_HEIGHT + (int)pos.x;
        availableNodes[index] = true;
    }

    public int AddExitToRoom(Rooms room)
    {
        int exitWall = Random.Range(0, room.freeWalls);
        int count = 0;
        bool hasFoundExit = false;
        while (!hasFoundExit)
        {
            for (int j = 0; j < 4; j++)
            {
                if (room.roomLayout[j] == 0)
                {
                    if (count == exitWall)
                    {
                        if (!CheckExit(room.GridPos, j, availableNodes))
                        {
                            room.freeWalls--;
                            room.roomLayout[j] = -1;
                            break;
                        }
                        hasFoundExit = true;
                        room.roomLayout[j] = 2;
                        exitWall = j;
                        break;
                    }
                    count++;
                }
            }
            if (room.freeWalls <= 0)
                return -1;
        }
        room.freeWalls--;
        return exitWall;
    }

    private bool CheckExit(Vector2 roomPos, int exit, bool[] availableNodes)
    {
        Vector2 newPos = roomPos + Directions[exit]; 
        if (newPos.x < 0 || newPos.y < 0 || newPos.y >= MAP_HEIGHT || newPos.x >= MAP_WIDTH)
            return false;

        int index = (int)newPos.y * MAP_HEIGHT + (int)newPos.x;
        if (index >= availableNodes.Length || availableNodes[index] == true)
            return false;
        return true;
    }

    private void AddBossRoom(List<Rooms> rooms)
    {
        bool hasSetBoosDoor = false;
        for (int i = rooms.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < 4; j++)
            {
                if (rooms[i].roomLayout[j] == 0)
                {
                    rooms[i].roomLayout[j] = 3;
                    hasSetBoosDoor = true;
                    break;
                }
            }
            if (hasSetBoosDoor)
                break;
        }
    }  

    private void DrawDots()
    {
        for (int i = 0; i < allAvailableRooms.Count; i++)
        {
            GameObject dot = Instantiate(DOT, this.transform);
            dot.transform.position = allAvailableRooms[i].GridPos;
        }
    }
}
