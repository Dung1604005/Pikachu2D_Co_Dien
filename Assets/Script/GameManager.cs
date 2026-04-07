using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GridManager gridManager;

    [SerializeField] private TimeManager timeManager;

    public static GameManager Instance;

    void Awake()
    {
        // Set up singleton
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
    }

    public void OnInit()
    {
        // Set time for match is 300s
        timeManager.OnInit(300);
        gridManager.OnInit();
    }
    void Start()
    {
        OnInit();
    }

}
