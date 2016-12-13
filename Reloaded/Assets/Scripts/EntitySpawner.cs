using UnityEngine;
using System.Collections.Generic;

public class EntitySpawner : MonoBehaviour {

    [SerializeField]
    [Range(0,1)]
    private float c_spawningFrequency = 0.01f;
    [SerializeField]
    private AnimationCurve c_groupingFrequency = null;
    [SerializeField]
    private int c_maxEntitiesPerSpawn = 3;
    [SerializeField]
    private GameObject c_entityToSpawn = null;
    private List<GameObject> c_entitiesPool;
    [SerializeField]
    private Camera c_camera = null;

    [SerializeField]
    private float c_spawningOffset = 1;
    private float c_topBound;
    private float c_bottomBound;
    private float c_rightCameraBound;

    void Start()
    {
        c_entitiesPool = new List<GameObject>();
        c_topBound    = gameObject.transform.position.y + c_spawningOffset;
        c_bottomBound = gameObject.transform.position.y - c_spawningOffset;
        c_rightCameraBound = c_camera.transform.position.x + c_camera.GetComponent<Camera>().aspect * c_camera.GetComponent<Camera>().orthographicSize;
    }

	void Update()
	{
        float t_random = Random.value;
        //decide if we spawn
        if (t_random < c_spawningFrequency)
        {
            //decide how many entities we spawn
            int t_entitiesToSpawn = Mathf.RoundToInt(c_maxEntitiesPerSpawn * c_groupingFrequency.Evaluate(t_random));
            ++t_entitiesToSpawn;//ensure we at least spawn one entitie

            for (int t_entitiesCount = 0; t_entitiesCount < t_entitiesToSpawn; t_entitiesCount++)
            {
                GameObject t_entity = SpawnEntity();
                Vector3 t_newPosition = Vector3.zero;
                t_newPosition.y = Random.Range(c_bottomBound, c_topBound);
                t_newPosition.x = gameObject.transform.position.x;
                t_entity.transform.position = t_newPosition;
            }
        }
	}

    private GameObject SpawnEntity()
    {
        GameObject t_spawned;
        if (c_entitiesPool.Count > 0)
        {
            t_spawned = c_entitiesPool[0];
            c_entitiesPool.RemoveAt(0);
            t_spawned.SetActive(true);
        }
        else
        {
            t_spawned = Instantiate(c_entityToSpawn, gameObject.transform) as GameObject;
            t_spawned.GetComponent<EntitySimpleMovement>().Bound = c_rightCameraBound;
            t_spawned.GetComponent<EntitySimpleMovement>().onExitBounds = DeleteEntity;
        }
        return t_spawned;
    }

    private void DeleteEntity(GameObject p_entity)
    {
        p_entity.SetActive(false);
        c_entitiesPool.Add(p_entity);
    }
}