using UnityEngine;

public class Player : MonoBehaviour {
    public enum Decision
    {
        none,
        ammo,
        health,
        attack
    }
    public Decision c_lastDecision
    {
        set;
        get;
    }
    private Animator c_playerAnimator;
    
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

    void Awake()
    {
        c_playerAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        c_lastDecision = Decision.none;
        Health = 1;
        Ammo = 0;
        Damage = 1;
    }

    public void IncreaseAmmo()
    {
        ++Ammo;
        c_lastDecision = Decision.ammo;
    }
    public void IncreaseHealth()
    {
        ++Health;
        c_lastDecision = Decision.health;
    }

    public void MakeAction()
    {
        switch (c_lastDecision)
        {
            case Decision.ammo:
                c_playerAnimator.SetTrigger("Reload");
                ++Ammo;
                break;
            case Decision.health:
                c_playerAnimator.SetTrigger("Heal");
                ++Health;
                break;
            case Decision.attack:
                c_playerAnimator.SetTrigger("Attack");
                break;
        }
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
}