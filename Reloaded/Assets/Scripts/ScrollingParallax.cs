using UnityEngine;

public class ScrollingParallax : MonoBehaviour {

    #region Variables
    [SerializeField]
    private SpriteRenderer[] c_sprites;

    [SerializeField]
    private float[] c_speeds;

    [SerializeField]
    private Transform c_camera;

    private float c_leftCameraBound;
    private float c_doubleCameraWidth;
    #endregion

    void Start()
    {
        c_doubleCameraWidth = c_camera.GetComponent<Camera>().aspect * c_camera.GetComponent<Camera>().orthographicSize*4;
        c_leftCameraBound = c_camera.transform.position.x - c_camera.GetComponent<Camera>().aspect * c_camera.GetComponent<Camera>().orthographicSize;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        for (int t_index = 0; t_index < c_speeds.Length; t_index++)
        {
            //move the sprites
            Vector3 t_movement = Vector3.left * c_speeds[t_index] * Time.deltaTime;
            //one sprite
            c_sprites[t_index * 2].transform.position += t_movement;
            CheckIfInsideBounds(c_sprites[t_index * 2]);
            //and its clone
            c_sprites[t_index * 2 + 1].transform.position += t_movement;
            CheckIfInsideBounds(c_sprites[t_index * 2 + 1]);
        }
    }

    void CheckIfInsideBounds(SpriteRenderer p_sprite)
    {
        if (p_sprite.bounds.max.x <= c_leftCameraBound)
        {
            Vector2 t_spritePosition = p_sprite.transform.position;
            t_spritePosition.x += c_doubleCameraWidth;
            p_sprite.transform.position = t_spritePosition;
        }
    }
}