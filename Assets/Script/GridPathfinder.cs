using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathfinder 
{
    private GridManager gridManager;


    int[] offsetX = {-1, 1, 0 ,0};
    int[] offsetY = {0, 0 , 1, - 1};


    public void OnInit(GridManager _gridManager)
    {
        gridManager = _gridManager;
    }

    // Check if point at (x1, y) can go to (x2, y)
    private bool CheckLineHaveSameY(int y, int x1, int x2)
    {
        int minX = Math.Min(x1,x2);
        int maxX = Math.Max(x1, x2);

        for(int x = minX + 1; x < maxX; x++)
        {
            if(!gridManager.GridState[x, y])
            {
                return false;
            }
        }
        return true;
    }
    // Check if point at (x, y1) can go to (x, y2)

    private bool CheckLineHaveSameX(int x, int y1, int y2)
    {
        int minY = Math.Min(y1,y2);
        int maxY = Math.Max(y1, y2);

        for(int y = minY + 1; y< maxY; y++)
        {
            if(!gridManager.GridState[x, y])
            {
                return false;
            }
        }
        return true;
    }



    
    // Check 4 case 2 point can connect
    // Case 1: Start connect to End by I Shape
    //Case 2: Start connect to End by L Shape
    // Case 3: Start connect to End by U or Z Shape
    

    public bool CheckIShape(Vector2Int startPosition, Vector2Int endPosition)
    {
        // If 2 position have the same X then check if they can connect 
        if(startPosition.x == endPosition.x)
        {
            if(CheckLineHaveSameX(startPosition.x , startPosition.y, endPosition.y))
            {
                return true;
            }
        }
        // If 2 position have the same y then check if they can connect 
        else if(startPosition.y == endPosition.y)
        {
            if(CheckLineHaveSameY(startPosition.y, startPosition.x, endPosition.x))
            {
                return true;
            }
        }
        // If 2 position can't satisfied 2 case above then I Shape is not valid 
        return false;
    }

    public bool CheckLShape(Vector2Int p1, Vector2Int p2)
    {
        //We want to check L shape connect from p1 to p2
        // => there must be the intersect of p1.x and p2.y
        // Or the intersect of p1.y and p2.x
        // we will check two point and check if there is a connect between them

        //Case 1: intersect of p1.x and p2.y, we call this p3
        Vector2Int p3 = new Vector2Int(p1.x, p2.y);
        //Check if p1 can connect to p3 and p2 can connect to p3 and p3 is empty
        if(gridManager.GridState[p3.x, p3.y] && CheckLineHaveSameX(p1.x, p1.y, p3.y) && CheckLineHaveSameY(p2.y, p2.x, p3.x))
        {
            // If p1 can connect to p3 and p3 can connect to p2
            // => p1 can connect to p2 by L shape
            return true;
        }

        // Same with Case 2: intersect of p1.y and p2.x
        p3 = new Vector2Int(p2.x, p1.y);

        if(gridManager.GridState[p3.x, p3.y] && CheckLineHaveSameX(p2.x, p2.y, p3.y) && CheckLineHaveSameY(p1.y, p1.x, p3.x))
        {
            return true;
        }

        // If 2 case can't satisfied then there is no choice to connect p1 to p2 by L Shape
        return false;

    }

    public bool CheckUShape(Vector2Int p1, Vector2Int p2)
    {
        // We want to check U or Z Shape connect from p1 to p2
        // => We must find 2 point (we call this as p3 and p4) which satisfied  the condition below:
        // 1. (p3.x = p1.x and p4.x = p2.x and p3.y = p4.y) or (p3.y = p1.y and p4.y = p2.y and p3.x = p4.x)
        // 2. p1 can connect to p3, p3 can connect p4, p4 can connect to p2 and p3, p4 is Empty


        // Case 1: p3 and p4 have the save Y
        // We need to go for every y possible
        for(int y = 0; y < gridManager.GridState.GetLength(1); y++)
        {
            Vector2Int p3 = new Vector2Int(p1.x, y);
            Vector2Int p4 = new Vector2Int(p2.x, y);
            if(gridManager.GridState[p3.x, p3.y] && gridManager.GridState[p4.x, p4.y] 
            && CheckLineHaveSameX(p3.x, p1.y, p3.y) && CheckLineHaveSameX(p4.x, p2.y, p4.y)
            && CheckLineHaveSameY(y, p3.x, p4.x))
            {
                // If find a possible p3 and p4 => can find a way with U shape
                return true;
            }
        }

        // Case 2: p3 and p4 have the same X
        // We need to go for every x possible

        for(int x = 0; x < gridManager.GridState.GetLength(0); x++)
        {
            Vector2Int p3 = new Vector2Int(x, p1.y);
            Vector2Int p4 = new Vector2Int(x, p2.y);

             if(gridManager.GridState[p3.x, p3.y] && gridManager.GridState[p4.x, p4.y] 
            && CheckLineHaveSameY(p3.y, p1.x, p3.x) && CheckLineHaveSameY(p4.y, p2.x, p4.x)
            && CheckLineHaveSameX(x, p3.y, p4.y))
            {
                // If find a possible p3 and p4 => can find a way with U shape
                return true;
            }
        }

        return false;

    }

    //Check if we can go from startPosition to EndPosition

    public bool CanConnect(Vector2Int startPosition, Vector2Int endPosition)
    {
        return true;
        
    }
}
