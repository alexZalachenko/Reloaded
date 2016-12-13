using UnityEngine;

public class GameController : MonoBehaviour {

    #region Variables
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
    private EnemyDecisionMaker c_enemyDecisionMaker = null;
    [SerializeField]
    private Player c_controlled = null;
    [SerializeField]
    private Player c_enemy = null;
    [SerializeField]
    private Clock c_clock = null;
    [SerializeField]
    private float c_delayUntilGameStarts = 2;
    [SerializeField]
    private float c_delayBetweenRounds = 1;
    private int c_animationsEnded = 0;
    #endregion

    void Awake()
    {
        GameObject.Find("Clock").GetComponent<Clock>().c_onCountDownEnd += OnCountDownEnd;

        GameObject[] t_players = GameObject.FindGameObjectsWithTag("Player");
        t_players[0].GetComponent<Player>().c_onAnimationEnded += OnPlayerAnimationEnded;
        t_players[1].GetComponent<Player>().c_onAnimationEnded += OnPlayerAnimationEnded;
    }

    void Start()
    {
        Invoke("NewRound", c_delayUntilGameStarts);
    }

    private void OnCountDownEnd()
    {
        //get the player decision from AI or from network
        c_enemy.c_lastDecision = c_enemyDecisionMaker.GetLastDecision();
        MakeDamageToPlayers();
        //play propper animation and heal or reload
        MakePlayersActions();
    }

    private void OnPlayerAnimationEnded()
    {
        ++c_animationsEnded;
        if (c_animationsEnded == 2)
        {
            c_animationsEnded = 0;
            if (!CheckIfGameEnded())
                Invoke("NewRound",c_delayBetweenRounds);
            else
                c_onGameEnd(c_controlled.Health > 0 ? "Victory" : "Defeat");
        }
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

    private void MakeDamageToPlayers()
    {
        //make damage if one player attacked
        if (c_controlled.c_lastDecision == Player.Decision.attack && c_enemy.c_lastDecision != Player.Decision.block)
            c_enemy.LoseHealth(c_controlled.Damage);
        if (c_enemy.c_lastDecision == Player.Decision.attack && c_controlled.c_lastDecision != Player.Decision.block)
            c_controlled.LoseHealth(c_enemy.Damage);
    }

    private void MakePlayersActions()
    {
        c_controlled.MakeAction();
        c_enemy.MakeAction();
    }
}