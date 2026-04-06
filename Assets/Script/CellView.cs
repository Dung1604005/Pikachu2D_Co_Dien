using System;
using UnityEngine;
using UnityEngine.EventSystems;
[System.Serializable]
public class CellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteEffectChoosed;

    [SerializeField] private SpriteRenderer spriteCell;

    [SerializeField] private CellConfig currentCellConfig;

    private bool isEmpty;

    public bool IsEmpty => isEmpty;
    public void OnInit(CellConfig _cellConfig)
    {
        isEmpty = false;
        this.gameObject.SetActive(true);
        currentCellConfig = _cellConfig;
        spriteCell.color = _cellConfig.ColorCell;
    }

    public void OnDeSpawn()
    {
        isEmpty = true;
        this.gameObject.SetActive(false);
    }
    public void SetChoosed()
    {

        // If this cell is choosed => Turn on effect choosed cell
        spriteEffectChoosed.enabled = true;
    }

    public void SetUnChoosed()
    {
        // If this cell isn't choosed any more => turn off effect choosed cell
        spriteEffectChoosed.enabled = false;
    }
}
