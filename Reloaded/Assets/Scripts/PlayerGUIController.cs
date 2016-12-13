using UnityEngine;
using UnityEngine.UI;

public class PlayerGUIController : MonoBehaviour {

    [SerializeField]
    private Text c_healthText = null;
    [SerializeField]
    private Text c_ammoText = null;

    private int c_health = 1;
    public int Health
    {
        set
        {
            c_health = value;
            c_healthText.text = c_health.ToString();
        }
        get
        {
            return c_health;
        }
    }

    private int c_ammo = 0;
    public int Ammo
    {
        set
        {
            c_ammo = value;
            c_ammoText.text = c_ammo.ToString();
        }
        get
        {
            return c_ammo;
        }
    }

    void Start()
    {
        c_healthText.text = c_health.ToString();
        c_ammoText.text   = c_ammo.ToString();
    }
}