using UnityEngine;

public class EntitySimpleMovement : MonoBehaviour {

    #region VARIABLES
    [SerializeField]
    private float c_speed = 1;
    [SerializeField]
    private float c_rotationSpeed = 1;
    [SerializeField]
    private bool c_rotates = false;

    public float Bound
    {
        set;
        get;
    }

    public delegate void OnExitBounds(GameObject p_entity);
    public OnExitBounds onExitBounds;
    #endregion

    void Update()
	{
        transform.position += Vector3.right * Time.deltaTime * c_speed;
        if (c_rotates)
        {
            transform.eulerAngles += Vector3.back * Time.deltaTime * c_rotationSpeed;
        }
        if (transform.position.x > Bound)
            onExitBounds(gameObject);
	}
}