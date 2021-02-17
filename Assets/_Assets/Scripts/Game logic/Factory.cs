using UnityEngine;


public abstract class Factory : MonoBehaviour{

    [SerializeField]
    private PoolOfEntities pool;


    /// <summary>
    /// @return
    /// </summary>
    protected abstract Transform GetRespawn();

    /// <summary>
    /// @return
    /// </summary>
    protected void InstantiateEntity()
    {
        GameObject entity = pool.GetEntity(GetRespawn());
        ConfigEntity(entity);
    }

    /// <summary>
    /// @return
    /// </summary>
    protected abstract void ConfigEntity(GameObject entity);

}