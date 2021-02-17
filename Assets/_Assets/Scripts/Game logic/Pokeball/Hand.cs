using UnityEngine;
using UnityEngine.Serialization;

public class Hand : Factory {

    [SerializeField]
    private Transform handAxis;

    [SerializeField]
    private float respawnRateTime;
    private float currentTime;

    private bool handIsEmpty, launched;
    private Pokeball lastPokeball;


    [SerializeField] private float launchForce;
    
    private void Start()
    {
        currentTime = respawnRateTime;
        handIsEmpty = true;
    }
    public void Launch()
    {
        if (lastPokeball.Launch(launchForce * this.transform.forward))
        {
            handIsEmpty = true;
        }
    }
    
    private void Update()
    {
        if (handIsEmpty)
        {
            if (currentTime >= respawnRateTime)
            {
                InstantiateEntity();
                currentTime = 0;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    protected override Transform GetRespawn()
    {
        return handAxis;
    }

    protected override void ConfigEntity(GameObject entity)
    {
        lastPokeball = entity.GetComponent<Pokeball>();
        lastPokeball.ConfigEntity();
        
        handIsEmpty = false;
    }

}