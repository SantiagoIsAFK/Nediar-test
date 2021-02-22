using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// Pool to store and manage the entities, Pokemon and Pokeball
    /// </summary>
    public class PoolOfEntities : MonoBehaviour
    {
        // <summary> Prefab </summary>
        [SerializeField] private GameObject entity;


        [SerializeField] private List<GameObject> entitiesList = new List<GameObject>();


        /// <summary>
        /// Return an instance of the prefab, if there are no enough, then, it creates one
        /// </summary>
        /// <param name="tf">transform of the respawn point</param>
        /// <returns>instance</returns>
        public GameObject GetEntity(Transform tf)
        {
            //Search a instance inactive to identify a entity without use
            for (int i = 0; i < entitiesList.Count; i++)
            {
                if (!entitiesList[i].activeInHierarchy)
                {
                    entitiesList[i].transform.SetParent(tf);
                    entitiesList[i].transform.localPosition = Vector3.zero;
                    entitiesList[i].transform.localRotation = Quaternion.identity;
                    entitiesList[i].SetActive(true);
                    return entitiesList[i]; //Break the function
                }
            }

            //if still in the function, then, it create one
            GameObject t_entity = Instantiate(entity, tf);
            entitiesList.Add(t_entity);

            return t_entity;
        }
    }
}