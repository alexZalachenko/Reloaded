using UnityEngine;

public class Player : MonoBehaviour {
    #region Variables
    public enum Decision
    {
        none,
        ammo,
        attack,
        block,
        superAttack
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
    [SerializeField]
    private int c_requiredAmmoSuperAttack = 5;

    [SerializeField]
    private bool c_isAI = false;
    [SerializeField]
    private bool c_dumbAI = false;
    [SerializeField]
    private ParticleSystem c_playerParticles;
    #endregion

    void Awake()
    {
        if (c_isAI)
            GameObject.Find("GameController").GetComponent<GameController>().c_onCountdownStart += MakeRandomDecision;
        c_playerAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        c_lastDecision = Decision.none;
        Health = 1;
        Ammo = 0;
        Damage = 1;
    }
    private void Update()
    {
        if (c_dumbAI)
        {
            
        }
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
                if (Ammo == c_requiredAmmoSuperAttack)
                    c_playerParticles.Play();
                break;
            case Decision.attack:
                c_playerAnimator.SetTrigger("Attack");
                --Ammo;
                c_playerGUIController.Ammo = Ammo;
                if (Ammo < c_requiredAmmoSuperAttack)
                    c_playerParticles.Stop();
                break;
            case Decision.superAttack:
                c_playerAnimator.SetTrigger("Attack");
                Ammo -= c_requiredAmmoSuperAttack;
                c_playerGUIController.Ammo = Ammo;
                if (Ammo < c_requiredAmmoSuperAttack)
                    c_playerParticles.Stop();
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
        if (Ammo >= c_requiredAmmoSuperAttack)
            c_lastDecision = Decision.superAttack;
        c_lastDecision = Decision.attack;
    }
    public void Block()
    {
        c_lastDecision = Decision.block;
    }
    public void IncreaseAmmo()
    {
        c_lastDecision = Decision.ammo;
    }

    public void SetHealth(int p_health)
    {
        Health = p_health;
        c_playerGUIController.Health = Health;
    }

    public void SetAmmo(int p_ammo)
    {
        Ammo = p_ammo;
        c_playerGUIController.Ammo = Ammo;
    }

    public void MakeRandomDecision()
    {
        if (c_dumbAI)
        {
            c_lastDecision = Decision.none;
            return;
        }

        if (Ammo == c_requiredAmmoSuperAttack)
        {
            c_lastDecision = Decision.attack;
            return;
        }
        
        int t_decision = Random.Range(0, 3);
        if (Ammo == 0)
            t_decision = Random.Range(0, 2);
        switch (t_decision)
        {
            case 0:
                c_lastDecision = Decision.ammo;
                break;
            case 1:
                c_lastDecision = Decision.block;
                break;
            case 2:
                c_lastDecision = Decision.attack;
                break;
        }
    }
}