using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;
    private Slider timeSlider;

    void Awake()
    {
        timeSlider = GetComponent<Slider>();

    }
    public void SetTimeValueSlider(float currentTimeValue, float matchTime)
    {
        
        timeSlider.value = Mathf.Lerp(timeSlider.value, currentTimeValue/matchTime, 0.1f);

    }


}
