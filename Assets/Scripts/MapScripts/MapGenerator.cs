using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    public int mapSeed;
    public bool isMapOfTheDay = true;
    
    private Room[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        // Get Map of the Day
        if (isMapOfTheDay)
        {
            mapSeed = DateToInt(DateTime.Now.Date);
        }

        // Set our Seed
        UnityEngine.Random.InitState(mapSeed);

        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Return a Random Room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    // Generate the Map
    public void GenerateMap()
    {
        // Clear out the Grid - "Column" = X, "Row" = Z
        grid = new Room[cols, rows];

        // For each Grid Row
        for (int cr = 0; cr < rows; cr++)
        {
            // For each Grid Column
            for (int cc = 0; cc < cols; cc++)
            {
                // Figure out Location
                float xPos = roomWidth * cc;
                float zPos = roomHeight * cr;
                Vector3 newPosition = new Vector3(xPos, 0.0f, zPos);

                // Create a new Grid at the Appropriate Location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                // Set its Parent
                tempRoomObj.transform.parent = this.transform;

                // Give it a Meaninful Name
                tempRoomObj.name = "Room " + cc + " , " + cr;

                // Get the Room Object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                // If we are on the --- open North Door
                if (cr == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                // If we are on the --- open South Door
                else if (cr == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                // If we are in the Middle open Both Doors
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }

                // If we are in the Left Column open East Door
                if (cc == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                // If we are in the Right Column open West Door
                else if (cc == cols - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                // If we are in the Middle open Both Doors
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }

                // Save to the Grid Array
                grid[cc, cr] = tempRoom;
            }
        }
    }

    // Date to Int
    public int DateToInt(DateTime dateToUse)
    {
        // Add Date YYYY-MM-DD HR:MM:SS:MS
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }
}
