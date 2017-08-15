using UnityEngine;

public class OnGameEndScript : MonoBehaviour {
    private GameController.EndGameState c_gameResult;

    void Awake()
    {
        GameObject.Find("GameController").GetComponent<GameController>().c_onGameEnd += OnGameEnd;
        gameObject.SetActive(false);
    }

    private void OnGameEnd(GameController.EndGameState p_result)
    {
        c_gameResult = p_result;
        gameObject.SetActive(true);
        gameObject.transform.GetChild(0).Find(p_result.ToString()).gameObject.SetActive(true);
        
        GameManager t_gameManager = GameObject.Find("GameController").GetComponent<GameManager>();
        if (t_gameManager != null)
            t_gameManager.EnemyDefeated();
    }

    public void DisableGameEndedMenu()
    {
        gameObject.transform.GetChild(0).Find(c_gameResult.ToString()).gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}