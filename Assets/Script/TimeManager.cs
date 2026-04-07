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
