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

    [Header("List Cell Data")]

    [SerializeField] private List<CellData> listCellData;

    [Header("Reference")]

    [SerializeField] private GridPathfinder gridPathfinder;

    [SerializeField] private PathVisual pathVisual;

    
    #region Grid State 

    private CellView[,] gridCells;
    // This Grid State is store the current state of every cell, check if it is empty or not ?

    private bool[,] gridState;

    private Vector2Int currentChoosedCellPosition;
    
    // The dictionary help finding cellData by IdCellData
    private Dictionary<int, CellData> mapCellData = new Dictionary<int, CellData>();

    // This dictionary store activeCell group by IdCellData
    private Dictionary<int, List<Vector2Int>> activeCellsDictionary = new Dictionary<int, List<Vector2Int>>();

    #endregion

    #region Getter

    public bool[,] GridState => gridState;

    public PathVisual PathVisual => pathVisual;

    public List<CellData> ListCellData => listCellData;

    public Vector2Int GridSize => gridSize;

    public Vector2 CellSize => cellSize;

    public Vector2 GridOrigin => gridOrigin;

    #endregion

    #region HELPER METHOD

    
    public bool CellIsEmpty(Vector2Int gridPosition)
    {
        //Check if this grid Position is valid
        if (!CordinateConventor.GridPositionIsValid(gridPosition, gridSize))
        {
            return false;
        }
        // Check if this position is empty ?
        return gridState[gridPosition.x + 1, gridPosition.y + 1];
    }

    public CellView GetCellByGridPosition(Vector2Int gridPosition)
    {
        // Check if this position is valid and empty ?
        if (!CordinateConventor.GridPositionIsValid(gridPosition, gridSize))
        {
            return null;
        }
        if (CellIsEmpty(gridPosition))
        {
            return null;
        }
        return gridCells[gridPosition.x, gridPosition.y];
    }

    public List<int> GetAllIdCellActive()
    {
        List<int> result = new List<int>();
        // Loop for evert active idCell
        foreach(int idCell in activeCellsDictionary.Keys)
        {
            // For every idCell, check if there are how many active cell in grid have this idCell
            int numberOfCell = activeCellsDictionary[idCell].Count; 
            
            //Add active cell id in result
            for(int i = 0 ;  i < numberOfCell; i++)
            {
                result.Add(idCell);
            }
        }
        return result;
    }

    #endregion

    #region INIT/Shuffle 
    // Start Spawn new Grid 
    public void OnInit()
    {
        // Implement that no cell is choosed right now
        currentChoosedCellPosition = new Vector2Int(-1, -1);
        GenerateCellDataForGrid();
    }

    /// <summary>
    /// Using Fisher-Yates Shuffle to shuffle the list of cell data then push them into grid
    /// </summary>
    public void GenerateCellDataForGrid()
    {
        // Clear old active cell
        activeCellsDictionary.Clear();

        // First init all the grid is empty
        for(int posX = 0 ; posX < gridSize.x + 2; posX++)
        {
            for(int posY =0; posY < gridSize.y + 2; posY++)
            {
                gridState[posX, posY] = true;
            }
        }
        // Then init all the cell in the real gridCell is false because they all exist
        for(int posX = 1 ; posX < gridSize.x + 1; posX++)
        {
            for(int posY =1; posY < gridSize.y + 1; posY++)
            {
                gridState[posX, posY] = false;
            }      
        }

        int totalCell = gridSize.x * gridSize.y;

        int numbType = listCellData.Count;


        // Make every cell Type have the same cell in grid
        int cellPerType = totalCell / numbType;

        List<int> listCellDataInGrid = new List<int>();

        for (int typeId = 1; typeId <= numbType; typeId++)
        {
            // Generate cellPerType cell every cellType
            for (int i = 0; i < cellPerType; i++)
            {
                listCellDataInGrid.Add(typeId);
            }
        }
        
        ShuffleGrid(listCellDataInGrid);
       


    }

    public void ShuffleGrid(List<int> listCellDataInGrid)
    {
         //Using algorithm Fisher-Yates Shuffle to shuffle the list
        System.Random rnd = new System.Random();

        for (int j = listCellDataInGrid.Count - 1; j >= 0; j--)
        {
            int i = rnd.Next(0, j + 1);

            int temp = listCellDataInGrid[i];

            listCellDataInGrid[i] = listCellDataInGrid[j];

            listCellDataInGrid[j] = temp;
        }

        //Init CellData for cell in gridPoints form list after shuffle
        int index = 0;

        for (int posY = 0; posY < gridSize.y; posY++)
        {
            for (int posX = 0; posX < gridSize.x; posX++)
            {
                // If cell is empty => cannot Init on that cell so we skip this
                if(CellIsEmpty(new Vector2Int(posX, posY)))
                {   
                    continue;
                }

                CellData cellData = mapCellData[listCellDataInGrid[index]];
                gridCells[posX, posY].OnInit(cellData, new Vector2Int(posX, posY));
                gridCells[posX, posY].SetUnChoosed();
                index++;

                //Update Cell active in dictionary
                UpdateActiveCellMap(cellData.IdCellData, new Vector2Int(posX, posY), true);
            }
        }
    }
    #endregion

    #region UPDATE GRID
    

    public void UpdateCurrentCellChoosed(Vector2Int position)
    {
        // Cell 1 is the new cell is choosed
        // Cell 2 is the choosed cell before this
        CellView cell1 = GetCellByGridPosition(position);
        CellView cell2 = GetCellByGridPosition(currentChoosedCellPosition);
        //Avoid duplicate

        if(position.x == currentChoosedCellPosition.x && position.y == currentChoosedCellPosition.y)
        {
            return;
        }
        if (!CordinateConventor.GridPositionIsValid(position, gridSize))
        {
            cell1?.SetUnChoosed();
            //If previous choosed cell is not empty then make it unchoosed
            if(currentChoosedCellPosition.x >= 0 && currentChoosedCellPosition.y >= 0)
            {
                 cell2?.SetUnChoosed();
            }
            return;
        }
        if (CellIsEmpty(position))
        {
             cell1?.SetUnChoosed();
             //If previous choosed cell is not empty then make it unchoosed
            if(currentChoosedCellPosition.x >= 0 && currentChoosedCellPosition.y >= 0)
            {
                 cell2?.SetUnChoosed();
            }
            
            return;
        }
        // If previous dont have any cell is choosed.
        if(currentChoosedCellPosition.x < 0 && currentChoosedCellPosition.y < 0 )
        {
            //Mark this cell as the current choosed cell
            currentChoosedCellPosition = position;
        }
        else
        {
            // If 2 cell is different then cant connect
            if(cell1?.CurrentCellData.IdCellData !=
            cell2?.CurrentCellData.IdCellData)
            {
                
                //The connect is false
                cell1?.SetUnChoosed();
                cell2?.SetUnChoosed();

                // Mark the current choosed cell is empty
                currentChoosedCellPosition =  new Vector2Int(-1, -1);

                Debug.Log("This 2 cell dont have the same type");
                return;
            }

            // If previous have  cell is choosed.
            // Try to connect them
            if(gridPathfinder.CanConnect(currentChoosedCellPosition, position))
            {
                
                 
                // DeSpawn them and remove them from active cell
                UpdateActiveCellMap(cell1.CurrentCellData.IdCellData, position, false);
                UpdateActiveCellMap(cell2.CurrentCellData.IdCellData, position, false);

                
                cell1?.OnDeSpawn();
                cell2?.OnDeSpawn();

               

                // if 2 cell can be connected => make them empty
                gridState[position.x + 1, position.y + 1] = true;
                gridState[currentChoosedCellPosition.x + 1, currentChoosedCellPosition.y + 1] = true;

                //The grid is change 
                OnChangeGrid();
                

                //Update Point for game
                GameManager.Instance.AddPoint(20);
                GameManager.Instance.TimeManager.AddTime(5);
                Debug.Log("Connect success");

            }
            else
            {
                

                //The connect is false
                cell1?.SetUnChoosed();
                cell2?.SetUnChoosed();

                Debug.Log("Connect fail");

            }
            // Mark the current choosed cell is empty
            currentChoosedCellPosition =  new Vector2Int(-1, -1);
            
        }
    }

    public void UpdateActiveCellMap(int idCellData, Vector2Int gridPosition, bool isAdd)
    {
        if (isAdd)
        {
            // Add this cell

            // Check in dictionary had key Cell Data yet ?
            if (activeCellsDictionary.ContainsKey(idCellData))
            {
                // If key existed => add position
                activeCellsDictionary[idCellData].Add(gridPosition);
            }
            else
            {
                // If key dont exist => Init new List include this position cell
                activeCellsDictionary.Add(idCellData, new List<Vector2Int>{gridPosition});
            }
        }
        else
        {

            // Remove this cell
            if (activeCellsDictionary.ContainsKey(idCellData))
            {
                // Remove this cell from the currently active cell
                List<Vector2Int> activeCell = activeCellsDictionary[idCellData];
                activeCell.Remove(gridPosition);
            }
        }
    }

    /// <summary>
    /// This function is called when the grid is change
    /// </summary>
    public void OnChangeGrid()
    {
        // the grid is deadlock
        // Need to shuffle again
        if (!IsAnyMoveLeft())
        {
            // Get all cell active then shuffle the grid
            List<int> allIdCellActive = GetAllIdCellActive();
            ShuffleGrid(allIdCellActive);
        }
    }
    #endregion

    #region Check State Grid

    public bool IsAnyMoveLeft()
    {
        // Loop for every active cell
        foreach (var idGroup in activeCellsDictionary.Values)
        {
            // Bug
            if (idGroup.Count < 2) {
               Debug.LogError("ODD CELL TYPE");
               continue;
            }
            // Check every matching pair

            for(int i = 0 ; i < idGroup.Count - 1; i++)
            {
                for(int j = i + 1; j < idGroup.Count; j++)
                {
                    // Find a move => grid is not in deadlock state
                    // We just want to check, so set order draw = false
                    if(gridPathfinder.CanConnect(idGroup[i], idGroup[j], false))
                    {
                        return true;
                    }
                }
            }
        }
        // There is no move 
        return false;
    }

    #endregion

    void Awake()
    {
        
        // Start spawn every cell for grid
        gridCells = new CellView[gridSize.x, gridSize.y];

        // Grid state is a array to track the state of every cell (check if that cell is empty or not)
        // This grid include the border around the gridCells so it extend x by 2 and extend y by 2
        gridState = new bool[gridSize.x + 2, gridSize.y + 2];
        
         // Start spawn every cell for grid
        for (int posY = 0; posY < gridSize.y; posY++)
        {
            for (int posX = 0; posX < gridSize.x; posX++)
            {
                gridCells[posX, posY] = Instantiate(cellPrefab, new Vector2(gridOrigin.x + posX * cellSize.x + cellSize.x / 2f, gridOrigin.y + posY * cellSize.y + cellSize.y / 2f), Quaternion.identity, transform).GetComponent<CellView>();
                
            }
        }
        // Create a dictionary of cell Config for better finding cellConfig with IdCell

        foreach (CellData cellConfig in listCellData)
        {
            mapCellData.Add(cellConfig.IdCellData, cellConfig);
        }

        gridPathfinder= new GridPathfinder();

        gridPathfinder.OnInit(this);
    }

    #region DEBUG METHOD
    [ContextMenu("Shuffle")]

    public void DebugShuffle()
    {
        List<int> allIdCellActive = GetAllIdCellActive();
        ShuffleGrid(allIdCellActive);
    }
    #endregion
}
