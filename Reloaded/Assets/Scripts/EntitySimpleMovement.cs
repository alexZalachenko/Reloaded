using UnityEngine;

public class EntitySimpleMovement : MonoBehaviour {

    [SerializeField]
    private float c_speed = 1;
    
    public float Bound
    {
        set;
        get;
    }

    public delegate void OnExitBounds(GameObject p_entity);
    public OnExitBounds onExitBounds;

	void Update()
	{
        gameObject.transform.position += Vector3.right * Time.deltaTime * c_speed;
        if (gameObject.transform.position.x > Bound)
            onExitBounds(gameObject);
	}
}