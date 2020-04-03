using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I ended up not using this cuz im not really sure what wall thickening is

public class ThickWallGridLevel : GridLevel
{
    public ThickWallGridLevel(int width, int height) : base(width, height)
    {
        m_width = width;
        m_height = height;
        cells = new Connections[width, height];
        for (int i = 0; i < m_width; i++)
        {
            for (int j = 0; j < m_height; j++)
            {
                cells[i, j] = new Connections();
            }
        }
    }

    public bool canPlaceCorridor2(int x, int y, int dirn)
    {
        int from_dx = (int)NEIGHBORS[dirn].x;
        int from_dy = (int)NEIGHBORS[dirn].y;
        int[] vector = new int[3] { -1, 0, 1};

        foreach (int dx in vector)
        {
            if(dx == from_dx)
            {
                continue;
            }
            foreach(int dy in vector)
            {
                if(dy == from_dy)
                {
                    continue;
                }
                if (!canPlaceCorridor(x + dx, y + dy, dirn))
                {
                    return false;
                }
            }
        }
        return true;
    }

    void shuffle()
    {
        int n = NEIGHBORS.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int r = Random.Range(0, n);
            Vector3 copy = NEIGHBORS[r];
            NEIGHBORS[r] = NEIGHBORS[i];
            NEIGHBORS[i] = copy;
        }
    }

    public override Location makeConnection(Location location)
    {
        shuffle();

        int x = location.x;
        int y = location.y;

        foreach (Vector3 vectors in NEIGHBORS)
        {
            int dx = (int)vectors.x;
            int dy = (int)vectors.y;
            int dirn = (int)vectors.z;

            int nx = x + dx;
            int ny = y + dy;
            int fromDirn = 3 - dirn;

            if (canPlaceCorridor2(nx, ny, fromDirn))
            {
                cells[x, y].directions[dirn] = true;
                cells[nx, ny].inMaze = true;
                cells[nx, ny].directions[fromDirn] = true;
                return new Location(nx, ny);
            }
        }

        return null;
    }
}
