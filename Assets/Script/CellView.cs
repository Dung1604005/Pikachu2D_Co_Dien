using System;
using UnityEngine;
using UnityEngine.EventSystems;
[System.Serializable]
public class CellView : MonoBehaviour
{
    
    [SerializeField] private GameObject spriteEffectChoosed;

    [SerializeField] private SpriteRenderer spriteCell;

    [SerializeField] private CellData currentCellData;

    private GridManager gridManager;

    public CellData CurrentCellData => currentCellData;

    private Vector2Int gridPosition;

    // Init basic information for CellView

    public void OnInit(CellData _cellData, Vector2Int _gridPosition)
    {
        gridPosition  = _gridPosition;
        this.gameObject.SetActive(true);
        currentCellData = _cellData;
        spriteCell.sprite = _cellData.CellIcon;
    }

    public void OnDeSpawn()
    {
        this.gameObject.SetActive(false);
    }

    
    public void SetChoosed()
    {
        
        // If this cell is choosed => Turn on effect choosed cell
        spriteEffectChoosed.SetActive(true);

        //Call the manager to update current cell choosed
        gridManager.UpdateCurrentCellChoosed(gridPosition);
    }

    public void SetUnChoosed()
    {
        // If this cell isn't choosed any more => turn off effect choosed cell
        spriteEffectChoosed.SetActive(false);
    }

    void Awake()
    {
        gridManager = GetComponentInParent<GridManager>();
    }
}
