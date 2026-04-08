using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TimeBar timeBar;
    private float matchTime;
    private float timeLeft;

    public void OnInit(float _matchTime)
    {
        matchTime = _matchTime;
        timeLeft= matchTime;
    }
    public void AddTime(float _amount)
    {
        // Add time and dont let it over the matchTime
        timeLeft = Mathf.Min(matchTime, timeLeft + _amount);
    }
    void Update()
    {
        if (timeLeft <= 0.1f)
        {
            return;
        }
        timeLeft -= Time.deltaTime;
        timeBar.SetTimeValueSlider(timeLeft, matchTime);
        
    }
}
