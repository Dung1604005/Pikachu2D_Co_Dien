using UnityEngine;

public class CordinateConventor
{
    public static Vector2Int ConvertWorldPositionToGridPosition(Vector2 worldPosition, Vector2 gridOrigin,Vector2 gridSize, Vector2 cellSize)
    {
        if (!WorldPositionIsValid(worldPosition, gridOrigin, gridSize, cellSize))
        {
            return new Vector2Int(-1, -1);
        }

        // Convert world Position to grid Position
        int posX = Mathf.FloorToInt((worldPosition.x - gridOrigin.x) / cellSize.x);
        int posY = Mathf.FloorToInt((worldPosition.y - gridOrigin.y) / cellSize.y);


        return new Vector2Int(posX, posY);
    }

    public static Vector2 ConvertGridPositionToWorldPosition(Vector2Int gridPosition ,Vector2 gridOrigin, Vector2 gridSize,
     Vector2 cellSize, bool force = false)
    {
        if (!GridPositionIsValid(gridPosition, gridSize) && !force)
        {
            return new Vector2(-1, -1);
        }

        Vector2 worldPosition =  new Vector2(gridOrigin.x + gridPosition.x * cellSize.x + cellSize.x / 2f, gridOrigin.y + gridPosition.y * cellSize.y + cellSize.y / 2f);

        Debug.Log(gridPosition + " " + worldPosition);
        return worldPosition;
    }
     public static bool WorldPositionIsValid(Vector2 worldPositon, Vector2 gridOrigin, Vector2 gridSize, Vector2 cellSize)
    {
        Debug.Log(worldPositon);
        Vector2 mostLeftBottomPosition = gridOrigin;
        Vector2 mostRightUpPosition = gridOrigin + new Vector2(gridSize.x * cellSize.x, gridSize.y * cellSize.y);
        if (worldPositon.x < mostLeftBottomPosition.x || worldPositon.y < mostLeftBottomPosition.y || worldPositon.x > mostRightUpPosition.x || worldPositon.y > mostRightUpPosition.y)
        {
            
            return false;
        }
        
        return true;
    }

    public static bool GridPositionIsValid(Vector2Int gridPosition, Vector2 gridSize)
    {
        // Check if grid position is not outside the array
        if (gridPosition.x < 0 || gridPosition.x >= gridSize.x || gridPosition.y < 0 || gridPosition.y >= gridSize.y)
        {
            return false;
        }
        return true;
    }

}
