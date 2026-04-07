using UnityEngine;

public class TimeManager : MonoBehaviour
{
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
    }
}
