using System;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// Factory of gameObjects, this is use for Pokeballs and Pokemon instances
    /// </summary>
    public abstract class Factory : MonoBehaviour
    {
        [SerializeField] private PoolOfEntities pool;


        /// <summary>
        /// return a transform to instantiate the object
        /// </summary>
        /// <returns>Axis o point of spawn</returns>
        protected abstract Transform GetRespawn();

        
        
        /// <summary>
        /// Use a pool to obtain a specific entity
        /// </summary>
        protected void InstantiateEntity()
        {
            GameObject entity = pool.GetEntity(GetRespawn());
            ConfigEntity(entity);
        }

        /// <summary>
        /// Function to initialize the entity and end the build
        /// </summary>
        /// <param name="entity">prefab obtained in the pool</param>
        protected abstract void ConfigEntity(GameObject entity);
    }
}