using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Config")]
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 cellSize;

    // Origin Point is pivot of cell in the bottom left
    [SerializeField] private Vector2 gridOrigin;

    [SerializeField] private GameObject cellPrefab;
    private CellView[,] gridCells;

    private bool[,] gridState;

    public bool[,] GridState => gridState;

    [Header("List Cell Config")]

    [SerializeField] private List<CellConfig> listCellConfig;

    public List<CellConfig> ListCellConfig => listCellConfig;

    private Dictionary<int, CellConfig> mapCellConfig = new Dictionary<int, CellConfig>();

    public void GenerateCellConfigForGrid()
    {
        int totalCell = gridSize.x * gridSize.y;

        int numbType = listCellConfig.Count;


        // Make every cell Type have the same cell in grid
        int cellPerType = totalCell / numbType;

        List<int> listCellConfigInGrid = new List<int>();

        for (int typeId = 1; typeId <= numbType; typeId++)
        {
            // Generate cellPerType cell every cellType
            for (int i = 0; i < cellPerType; i++)
            {
                listCellConfigInGrid.Add(typeId);
            }
        }
        //Using algorithm Fisher-Yates Shuffle to shuffle the list
        System.Random rnd = new System.Random();

        for (int j = listCellConfigInGrid.Count - 1; j >= 0; j--)
        {
            int i = rnd.Next(0, j + 1);

            int temp = listCellConfigInGrid[i];

            listCellConfigInGrid[i] = listCellConfigInGrid[j];

            listCellConfigInGrid[j] = temp;
        }

        //Init CellConfig for cell in gridPoints form listCellConfigInGrid
        int index = 0;

        for (int posY = 0; posY < gridSize.y; posY++)
        {
            for (int posX = 0; posX < gridSize.x; posX++)
            {
                gridCells[posY, posX].OnInit(mapCellConfig[listCellConfigInGrid[index]]);
                index++;
            }
        }
    }

    public Vector2Int ConvertWorldPositionToGridPosition(Vector2 worldPosition)
    {
        if (!WorldPositionIsValid(worldPosition))
        {
            return new Vector2Int(-1, -1);
        }


        int posX = Mathf.FloorToInt((worldPosition.x - gridOrigin.x) / cellSize.x);
        int posY = Mathf.FloorToInt((worldPosition.y - gridOrigin.y) / cellSize.y);

        return new Vector2Int(posY, posX);
    }

    public bool CellIsEmpty(Vector2Int gridPosition)
    {
        //Check if this grid Position is valid
        if (!GridPositionIsValid(gridPosition))
        {
            return false;
        }
        // Check if this position is empty ?
        return gridCells[gridPosition.y, gridPosition.x].IsEmpty;
    }

    public bool WorldPositionIsValid(Vector2 worldPositon)
    {

        Vector2 mostLeftBottomPosition = gridOrigin;
        Vector2 mostRightUpPosition = gridOrigin + new Vector2(gridSize.x * cellSize.x, gridSize.y * cellSize.y);
        if (worldPositon.x < mostLeftBottomPosition.x || worldPositon.y < mostLeftBottomPosition.y || worldPositon.x > mostRightUpPosition.x || worldPositon.y > mostRightUpPosition.y)
        {
            return false;
        }
        return true;
    }

    public bool GridPositionIsValid(Vector2Int gridPosition)
    {
        // Check if grid position is not outside the array
        if (gridPosition.x < 0 || gridPosition.x >= gridSize.x || gridPosition.y < 0 || gridPosition.y >= gridSize.y)
        {
            return false;
        }
        return true;
    }


    public CellView GetCellByGridPosition(Vector2Int gridPosition)
    {
        if (!GridPositionIsValid(gridPosition))
        {
            return null;
        }
        if (CellIsEmpty(gridPosition))
        {
            return null;
        }
        return gridCells[gridPosition.x, gridPosition.y];
    }

    // Start Spawn new Grid 
    public void OnInit()
    {
        GenerateCellConfigForGrid();
    }

    void Awake()
    {
        // Start spawn every cell for grid
        gridCells = new CellView[gridSize.y, gridSize.x];

        // Grid state is a array to track the state of every cell (check if that cell is empty or not)
        // This grid include the border around the gridCells so it extend x by 2 and extend y by 2
        gridState = new bool[gridSize.y + 2, gridSize.x + 2];
        for (int posY = 0; posY < gridSize.y; posY++)
        {
            for (int posX = 0; posX < gridSize.x; posX++)
            {
                gridCells[posY, posX] = Instantiate(cellPrefab, new Vector2(gridOrigin.x + posX * cellSize.x + cellSize.x / 2f, gridOrigin.y + posY * cellSize.y + cellSize.y / 2f), Quaternion.identity, transform).GetComponent<CellView>();

            }
        }
        // Create a dictionary of cell Config for better finding cellConfig with IdCell

        foreach (CellConfig cellConfig in listCellConfig)
        {
            mapCellConfig.Add(cellConfig.IdCell, cellConfig);
        }
    }

    void Start()
    {
        OnInit();
    }
}
