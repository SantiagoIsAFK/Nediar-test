using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl  : MonoBehaviour
{
    
    [SerializeField]
    private Authentication auth;

    private void Awake()
    {
        if (auth != null)
        {
            auth.loggedIn += StartGame;
        }
    }

    public void StartGame()
    {
        auth.loggedIn -= StartGame;
        SceneManager.LoadScene("Game");
    }

    public void CloseApp()
    {
        Application.Quit();
    }
    
}