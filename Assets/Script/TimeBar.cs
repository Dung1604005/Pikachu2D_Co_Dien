using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    private Slider timeSlider;

    #region Timer

    private float targetTimeValue;

    private float currentTimeValue;

    #endregion

    void Awake()
    {
        timeSlider = GetComponent<Slider>();

    }
    public void SetTargetTimeValue()
    {
        
    }


}
