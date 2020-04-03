using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int mazeWidth;
    public int mazeHeight;
    public Location mazeStart = new Location(0, 0);
    Location exit;

    GridLevel levelOne;
    public GameObject finish;
    public GameObject wallPrefab;

    void Start()
    {
        levelOne = new GridLevel(mazeWidth, mazeHeight);
        exit = new Location(mazeWidth - 1 - mazeStart.x, mazeHeight - 1 - mazeStart.y);
        finish.transform.position = new Vector3(mazeWidth - mazeStart.x, 0, mazeHeight - mazeStart.y - 1);
        GenerateMaze(levelOne, mazeStart);
        MakeDoorway(mazeStart);
        MakeDoorway(exit);
        BuildMaze();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (GameObject wall in walls)
            {
                Destroy(wall);
            }

            mazeWidth = 10; //(int)Random.Range(5f, 10f);
            mazeHeight = 10; //(int)Random.Range(10f, 20f);
            levelOne = new GridLevel(mazeWidth, mazeHeight);

            GenerateMaze(levelOne, mazeStart);
            Location exit = new Location(mazeWidth - 1 - mazeStart.x, mazeHeight - 1 - mazeStart.y);
            MakeDoorway(mazeStart);
            MakeDoorway(exit);
            BuildMaze();
        }
        /*
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                Connections currentCell = levelOne.cells[x, y];
                if (levelOne.cells[x, y].inMaze)
                {
                    Vector3 cellPos = new Vector3(x, 0, y);
                    float lineLength = 1f;
                    if (!currentCell.directions[0])
                    {
                        // positive x
                        Vector3 neighborPos = new Vector3(x + lineLength, 0, y);
                        Debug.DrawLine(cellPos, neighborPos, Color.cyan);
                    }
                    if (!currentCell.directions[1])
                    {
                        // positive y
                        Vector3 neighborPos = new Vector3(x, 0, y + lineLength);
                        Debug.DrawLine(cellPos, neighborPos, Color.cyan);
                    }
                    if (!currentCell.directions[2])
                    {
                        // negative y
                        Vector3 neighborPos = new Vector3(x, 0, y - lineLength);
                        Debug.DrawLine(cellPos, neighborPos, Color.cyan);
                    }
                    if (!currentCell.directions[3])
                    {
                        // negative x
                        Vector3 neighborPos = new Vector3(x - lineLength, 0, y);
                        Debug.DrawLine(cellPos, neighborPos, Color.cyan);
                    }
                }
            }
        }*/
    }

    void MakeDoorway(Location location)
    {
        Connections cell = levelOne.cells[location.x, location.y];
        // which connection to set to true?
        // directions are listed in this order: +x, +y, -y, -x
        if (location.x == 0)
        {
            cell.directions[3] = true;
        }
        else if (location.x == mazeWidth - 1)
        {
            cell.directions[0] = true;
        }
        else if (location.y == 0)
        {
            cell.directions[2] = true;
        }
        else if (location.y == mazeHeight - 1)
        {
            cell.directions[1] = true;
        }
    }

    void BuildMaze()
    {
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                Connections currentCell = levelOne.cells[x, y];
                if (levelOne.cells[x, y].inMaze)
                {
                    Vector3 cellPos = new Vector3(x, 0, y);
                    float lineLength = 1f;
                    if (!currentCell.directions[0])
                    {
                        Vector3 wallPos = new Vector3(x + lineLength / 2, 0, y);
                        GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.identity) as GameObject;
                    }
                    if (!currentCell.directions[1])
                    {
                        Vector3 wallPos = new Vector3(x, 0, y + lineLength / 2);
                        GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
                    }
                    if (x == 0 && !currentCell.directions[3])
                    {
                        Vector3 wallPos = new Vector3(x - lineLength / 2, 0, y);
                        GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.identity) as GameObject;
                    }
                    if (y == 0 && !currentCell.directions[2])
                    {
                        Vector3 wallPos = new Vector3(x, 0, y - lineLength / 2);
                        GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
                    }
                }
                
            }

        }
    }
    
    void GenerateMaze(Level level, Location start)
    {
        Stack<Location> locations = new Stack<Location>();
        locations.Push(start);
        level.startAt(start);

        while (locations.Count > 0)
        {
            Location current = locations.Peek();

            Location next = level.makeConnection(current);
            if (next != null)
            {
                locations.Push(next);
            }
            else
            {
                locations.Pop();
            }
        }
    }
}
