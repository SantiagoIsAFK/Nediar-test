using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl  : MonoBehaviour
{
    
    [SerializeField]
    private Authentication auth;

    private void Awake()
    {
        auth.loggedIn += StartGame;
        
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void CloseApp()
    {
        Application.Quit();
    }
    
}