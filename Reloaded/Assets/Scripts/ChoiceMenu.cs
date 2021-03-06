﻿using UnityEngine;
using UnityEngine.UI;

public class ChoiceMenu : MonoBehaviour {
    [SerializeField]
    private Player c_controlled = null;

    void Awake()
    {
        GameObject.Find("Clock").GetComponent<Clock>().c_onCountDownEnd += OnCountdownEnd;
        GameObject.Find("GameController").GetComponent<GameController>().c_onCountdownStart += OnCountdownStart;
        gameObject.SetActive(false);
    }

    private void OnCountdownEnd()
    {
        gameObject.SetActive(false);
    }
    private void OnCountdownStart()
    {
        gameObject.SetActive(true);
        EnableButtons();
    }

    public void DisableButtons()
    {
        for (int t_index = 0; t_index < gameObject.transform.childCount; t_index++)
            gameObject.transform.GetChild(t_index).GetComponent<Button>().interactable = false;
    }
    private void EnableButtons()
    {
        for (int t_index = 0; t_index < gameObject.transform.childCount; t_index++)
        {
            if (gameObject.transform.GetChild(t_index).name != "Attack")
                gameObject.transform.GetChild(t_index).GetComponent<Button>().interactable = true;
            else if (c_controlled.Ammo >= 1)
                gameObject.transform.GetChild(t_index).GetComponent<Button>().interactable = true;
            else
                gameObject.transform.GetChild(t_index).GetComponent<Button>().interactable = false;
        }
    }
}