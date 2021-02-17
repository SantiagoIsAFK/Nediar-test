using System;
using UnityEngine;


public class AutoDestroy   : MonoBehaviour
{

    private bool isActive=false;
    
    [SerializeField]
    private float lifetime;

    private float remainingLifetime;


    public void StartAutoDestroy()
    {
        remainingLifetime = lifetime;
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            ConsumeLifetime();
        }
    }

    private void ConsumeLifetime() {
        
        remainingLifetime -= Time.deltaTime;
        if (remainingLifetime <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        isActive = false;
        this.enabled = false;
    }

}