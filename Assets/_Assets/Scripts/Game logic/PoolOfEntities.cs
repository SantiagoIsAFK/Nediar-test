using System.Collections.Generic;
using UnityEngine;

public class PoolOfEntities   : MonoBehaviour 
{
    [SerializeField]
    private GameObject entity;
    
    [SerializeField]
    private float sizeMin;

    [SerializeField]
    private List<GameObject> entitiesList = new List<GameObject>();
    
    
    
    public GameObject GetEntity(Transform tf)
    {
        for (int i = 0; i < entitiesList.Count; i++)
        {
            if (!entitiesList[i].activeInHierarchy)
            {
                entitiesList[i].SetActive(true);
                return entitiesList[i];
            }
        }

        GameObject t_entity = Instantiate(entity, tf);
        entitiesList.Add(t_entity);

        return t_entity;
    }
}