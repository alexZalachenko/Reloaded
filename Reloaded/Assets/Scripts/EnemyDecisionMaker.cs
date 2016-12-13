using UnityEngine;

public class EnemyDecisionMaker : MonoBehaviour {

    public enum GameType
    {
        singleplayer,
        multiplayer
    }
    public GameType c_GameType
    {
        set;
        get;
    }

    [SerializeField]
    private Player c_enemy;
    [SerializeField]
    private bool c_dumbAI = false;

	public Player.Decision GetLastDecision()
    {
        if (c_dumbAI)
            return Player.Decision.none;

        int t_decision;
        if (c_enemy.Ammo == 0)
        {
            t_decision = Random.Range(0, 2);
        }
        else
            t_decision = Random.Range(0, 3);
        switch (t_decision)
        {
            case 0:
                return Player.Decision.ammo;
            case 1:
                return Player.Decision.block;
            case 2:
                return Player.Decision.attack;
        }
        return Player.Decision.none;
    }
}