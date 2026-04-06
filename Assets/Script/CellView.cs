using System;
using UnityEngine;
using UnityEngine.EventSystems;
[System.Serializable]
public class CellView : MonoBehaviour
{
    [SerializeField] private GameObject spriteEffectChoosed;

    [SerializeField] private SpriteRenderer spriteCell;

    [SerializeField] private CellData currentCellData;

    private bool isEmpty;

    public bool IsEmpty => isEmpty;
    public void OnInit(CellData _cellData)
    {
        isEmpty = false;
        this.gameObject.SetActive(true);
        currentCellData = _cellData;
        spriteCell.sprite = _cellData.CellIcon;
    }

    public void OnDeSpawn()
    {
        isEmpty = true;
        this.gameObject.SetActive(false);
    }
    public void SetChoosed()
    {

        // If this cell is choosed => Turn on effect choosed cell
        spriteEffectChoosed.SetActive(true);
    }

    public void SetUnChoosed()
    {
        // If this cell isn't choosed any more => turn off effect choosed cell
        spriteEffectChoosed.SetActive(false);
    }
}
