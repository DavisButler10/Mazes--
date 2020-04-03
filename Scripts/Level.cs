using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level
{
    public abstract void startAt(Location location);
    //public bool canPlaceCorridor(int x, int y, int dirn);
    public abstract Location makeConnection(Location location);
}

public class Location
{
    public int x;
    public int y;

    public Location(int xCoord, int yCoord)
    {
        x = xCoord;
        y = yCoord;
    }
}

public class Connections
{
    public bool inMaze = false;
    public bool[] directions = new bool[4] { false, false, false, false };
}
