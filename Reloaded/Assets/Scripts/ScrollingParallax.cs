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

        //clone all the background sprites, so when doing scrolling parallax the movement looks to be continous without gaps
        //SpriteRenderer[] t_spritesAux = c_sprites;
        //int t_prevLength = c_sprites.Length;
        //c_sprites = new SpriteRenderer[c_sprites.Length * 2];
        //for (int t_sprite = 0; t_sprite < c_sprites.Length; t_sprite++)
        //{
        //    if (t_sprite < t_spritesAux.Length)
        //        c_sprites[t_sprite] = t_spritesAux[t_sprite];
        //    else
        //    {
        //        c_sprites[t_sprite] = (Instantiate(c_sprites[t_sprite- t_prevLength].gameObject, transform) as GameObject).GetComponent<SpriteRenderer>();
        //        c_sprites[t_sprite].transform.position = new Vector2((c_sprites[t_sprite - t_prevLength].transform.position.x + c_sprites[t_sprite].sprite.bounds.extents.x * 2),
        //                                                                  c_sprites[t_sprite - t_prevLength].transform.position.y);
        //        c_sprites[t_sprite].color = Color.green;
        //    }
        //}
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