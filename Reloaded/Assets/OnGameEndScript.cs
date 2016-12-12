using UnityEngine;

public class OnGameEndScript : MonoBehaviour {
    private string c_gameResult;
    void Awake()
    {
        GameObject.Find("GameController").GetComponent<GameController>().c_onGameEnd += OnGameEnd;
        gameObject.SetActive(false);
    }

    private void OnGameEnd(string p_result)
    {
        c_gameResult = p_result;
        gameObject.SetActive(true);
        gameObject.transform.GetChild(0).FindChild(p_result).gameObject.SetActive(true);
    }

    public void DisableGameEndedMenu()
    {
        gameObject.transform.GetChild(0).FindChild(c_gameResult).gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}