using UnityEngine;


public abstract class Factory : MonoBehaviour{

    [SerializeField]
    private GameObject Entity;


    /// <summary>
    /// @return
    /// </summary>
    protected abstract Transform GetRespawn();

    /// <summary>
    /// @return
    /// </summary>
    private void InstantiateEntity() {
    }

    /// <summary>
    /// @return
    /// </summary>
    protected abstract void ConfigEntity();

}