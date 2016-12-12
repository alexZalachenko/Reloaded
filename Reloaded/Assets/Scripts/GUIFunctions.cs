using UnityEngine.SceneManagement;
using UnityEngine;

public class GUIFunctions : MonoBehaviour {

    public void LoadScene(string p_scene)
    {
        SceneManager.LoadScene(p_scene);
    }
}