using UnityEngine;

public class Player : MonoBehaviour {
    #region Variables
    public enum Decision
    {
        none,
        ammo,
        health,
        attack,
        block
    }
    public Decision c_lastDecision
    {
        set;
        get;
    }
    private Animator c_playerAnimator;

    [SerializeField]
    private PlayerGUIController c_playerGUIController = null;

    public delegate void OnAnimationEnded();
    public OnAnimationEnded c_onAnimationEnded
    {
        set;
        get;
    }

    public int Health
    {
        set;
        get;
    }
    public int Ammo
    {
        set;
        get;
    }
    public int Damage
    {
        set;
        get;
    }
    #endregion

    void Awake()
    {
        c_playerAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }
    void Start()
    {
        c_lastDecision = Decision.none;
        Health = 1;
        Ammo = 0;
        Damage = 1;
    }

    public void IncreaseAmmo()
    {
        c_lastDecision = Decision.ammo;
    }
    public void IncreaseHealth()
    {
        c_lastDecision = Decision.health;
    }
    public void LoseHealth(int p_damage)
    {
        Health -= p_damage;
        c_playerGUIController.Health = Health;
    }

    public void MakeAction()
    {
        switch (c_lastDecision)
        {
            case Decision.ammo:
                c_playerAnimator.SetTrigger("Reload");
                ++Ammo;
                c_playerGUIController.Ammo = Ammo;
                break;
            case Decision.health:
                c_playerAnimator.SetTrigger("Heal");
                ++Health;
                c_playerGUIController.Health = Health;
                break;
            case Decision.attack:
                c_playerAnimator.SetTrigger("Attack");
                --Ammo;
                c_playerGUIController.Ammo = Ammo;
                break;
            case Decision.block:
                c_playerAnimator.SetTrigger("Heal");
                break;
        }
        if (c_lastDecision == Decision.none)
            c_onAnimationEnded();
        c_lastDecision = Decision.none;
    }
    public void Die()
    {
        c_playerAnimator.SetTrigger("Die");
    }
    public void Attack()
    {
        c_lastDecision = Decision.attack;
    }
    public void Block()
    {
        c_lastDecision = Decision.block;
    }
}