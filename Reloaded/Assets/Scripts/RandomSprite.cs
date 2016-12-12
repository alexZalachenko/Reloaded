using UnityEngine;
using System.Collections;

public class RandomSprite : MonoBehaviour {

    [SerializeField]
    private Sprite[] c_sprites;
    [SerializeField]
    private SpriteRenderer c_spriteRenderer;

    void Awake()
    {
        c_spriteRenderer.sprite = c_sprites[Random.Range(0, c_sprites.Length)];
    }
}