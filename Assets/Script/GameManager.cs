using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Reference")]
    [SerializeField] private GridManager gridManager;

    [SerializeField] private TimeManager timeManager;

    [SerializeField] private UIManager uiManager;

    #region State Game Parameter
    
    // This is the number of time player can shuffle grid and use suggest
    private int timeShuffle;

    private int currentPoint;

    #endregion

    #region GETTER

    public int TimeShuffle => timeShuffle;

    public int CurrentPoint => currentPoint;
    #endregion

    
    /// <summary>
    /// Restart the game
    /// </summary>
    public void OnInit()
    {
        // Set time for match is 300s
        timeManager.OnInit(300);
        gridManager.OnInit();
        // Start with 5 time shuffle and 0 point
        timeShuffle  = 5;
        currentPoint = 0;
        //Update UI 
        uiManager.SetTextTimeShuffle(timeShuffle);
        uiManager.SetTextPoint(currentPoint);

    }

    public void AddPoint(int _amount)
    {
        // Add point and update UI
        currentPoint += _amount;
        uiManager.SetTextPoint(currentPoint);
    }

    public void ShuffleGrid()
    {
        if(timeShuffle > 0)
        {
            //If player still can shuffle then shuffle the grid
            timeShuffle-=1;
            gridManager.DebugShuffle();

        }
    }

    /// <summary>
    /// End the game
    /// </summary>
    public void GameOver()
    {
        
    }
    void Start()
    {
        OnInit();
    }

}
