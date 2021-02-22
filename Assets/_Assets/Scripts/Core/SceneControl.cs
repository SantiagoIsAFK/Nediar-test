using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    /// <summary>
    /// Control the changes of the scenes
    /// </summary>
    public class SceneControl : MonoBehaviour
    {
        [SerializeField] private Authentication auth; //firebase instance

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
}