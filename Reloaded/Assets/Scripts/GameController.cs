using UnityEngine;

public class GameController : MonoBehaviour {

    private static GameController c_singletonInstance;
    public static GameController Instance
    {
        get
        {
            return c_singletonInstance;
        }
    }

    public delegate void OnCountDownStart();
    public OnCountDownStart c_onCountdownStart
    {
        set;
        get;
    }

    public delegate void OnGameEnd(string p_result);
    public OnGameEnd c_onGameEnd
    {
        set;
        get;
    }

    [SerializeField]
    private Player c_controlled;
    [SerializeField]
    private Player c_enemy;
    [SerializeField]
    private Clock c_clock;
    [SerializeField]


    void Awake()
    {
        if (c_singletonInstance != null && c_singletonInstance != this)
            Destroy(gameObject);
        else
        {
            c_singletonInstance = this;
            DontDestroyOnLoad(gameObject);
            GameObject.Find("Clock").GetComponent<Clock>().c_onCountDownEnd += OnCountDownEnd;
        }
    }

    void Start()
    {
        OnCountDownEnd();
    }

    private void OnCountDownEnd()
    {
        //make damage if one player attacked
        if (c_controlled.c_lastDecision == Player.Decision.attack)
            c_enemy.Health -= c_controlled.Damage;
            
        if (c_enemy.c_lastDecision == Player.Decision.attack)
            c_controlled.Health -= c_enemy.Damage;

        //play propper animation and heal or reload
        c_controlled.MakeAction();
        c_enemy.MakeAction();

        if (!CheckIfGameEnded())
            Invoke("NewRound", 2);
        else
            c_onGameEnd(c_controlled.Health > 0 ? "Victory" : "Defeat");
    }

    private bool CheckIfGameEnded()
    {
        bool t_anyDeath = false;
        if (c_controlled.Health <= 0)
        {
            c_controlled.Die();
            t_anyDeath = true;
        }
        if (c_enemy.Health <= 0)
        {
            c_enemy.Die();
            t_anyDeath = true;
        }
        return t_anyDeath;
    }

    private void NewRound()
    {
        StartCoroutine(c_clock.StartCountDown());
        c_onCountdownStart();
    }


}