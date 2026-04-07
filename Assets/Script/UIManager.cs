using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private TimeBar timeBar;

    [SerializeField] private TextMeshProUGUI textTimeShuffle;

    [SerializeField] private TextMeshProUGUI textPoint;

    #region GETTER

    public TimeBar TimeBar => timeBar;

    #endregion

    public void ShuffleGrid()
    {
        GameManager.Instance.ShuffleGrid();
        SetTextTimeShuffle(GameManager.Instance.TimeShuffle);
    }
    public void SetTextTimeShuffle(int timeShuffle)
    {
        textTimeShuffle.text =": " + timeShuffle.ToString();
    }
    public void SetTextPoint(int point)
    {
        textPoint.text = "Point: " + point;
    }

    
}
