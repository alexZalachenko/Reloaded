using UnityEngine;

public class GameController : MonoBehaviour {

    #region Variables
    public delegate void OnCountDownStart();
    public OnCountDownStart c_onCountdownStart
    {
        set;
        get;
    }

    public enum EndGameState
    {
        Victory,
        Defeat
    }
    public delegate void OnGameEnd(EndGameState p_result);
    public OnGameEnd c_onGameEnd
    {
        set;
        get;
    }
    
    public Player c_controlled = null;
    public Player c_enemy = null;
    
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
        c_clock.GetComponent<Clock>().c_onCountDownEnd += OnCountDownEnd;
        c_controlled.GetComponent<Player>().c_onAnimationEnded += OnPlayerAnimationEnded;
        c_enemy.GetComponent<Player>().c_onAnimationEnded += OnPlayerAnimationEnded;
    }

    void Start()
    {
        GameObject t_gameManager = GameObject.Find("GameManager");
        if (t_gameManager != null)
        {
            GameManager t_gameManagerScript = t_gameManager.GetComponent<GameManager>();
            int t_param;
            int.TryParse(t_gameManagerScript.c_currentOpponent.c_health, out t_param);
            c_enemy.SetHealth(t_param);
            int.TryParse(t_gameManagerScript.c_currentOpponent.c_ammo, out t_param);
            c_enemy.SetAmmo(t_param);
        }

        if (gameObject.tag != "Multiplayer")
            Invoke("NewRound", c_delayUntilGameStarts);
    }

    public void StartGame()
    {
        Invoke("NewRound", c_delayUntilGameStarts);
    }

    public void AddPlayer(Player c_newPlayer)
    {
        if (c_controlled == null)
        {
            c_controlled = c_newPlayer;
            c_controlled.GetComponent<Player>().c_onAnimationEnded += OnPlayerAnimationEnded;
        }
        else
        {
            c_enemy = c_newPlayer;
            c_enemy.GetComponent<Player>().c_onAnimationEnded += OnPlayerAnimationEnded;
        }
    }

    private void OnCountDownEnd()
    {
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
                c_onGameEnd(c_controlled.Health > 0 ? EndGameState.Victory : EndGameState.Defeat);
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
        c_clock.StartCountDown();//StartCoroutine(c_clock.StartCountDown());//
        c_onCountdownStart();
    }

    private void MakeDamageToPlayers()
    {
        //make damage if one player attacked
        if ((c_controlled.c_lastDecision == Player.Decision.attack && c_enemy.c_lastDecision != Player.Decision.block) ||
             c_controlled.c_lastDecision == Player.Decision.superAttack)
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