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

    //Check if we can go from startPosition to EndPosition
    public bool CanConnect(Vector2Int startPosition, Vector2Int endPosition)
    {
        // Convert startPosition and endPosition from gridPoints Position to position in gridstate
        startPosition = new Vector2Int(startPosition.x + 1, startPosition.y + 1);
        endPosition = new Vector2Int(endPosition.x + 1, endPosition.y + 1);

        //Using bfs algorithm to search the shortest path from startPosition to endPosition
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(startPosition);

        while(queue.Count > 0)
        {
            Vector2Int currentPosition = queue.Peek();
            // Check 4 direction around current position
            for(int i = 0; i < 4; i++)
            {
                int nextX = currentPosition.x + offsetX[i];
                int nextY = currentPosition.y + offsetY[i];
                // Check if this next position is outside the array ?
                // If it outside then skip it
                if(nextX< 0 || nextY < 0 || nextX >= gridManager.GridState.GetLength(0) || nextY >= gridManager.GridState.GetLength(1))
                {
                    // Skip this nextPosition
                    continue;
                }

                
                
            }
        }

        return true;
    }
}
