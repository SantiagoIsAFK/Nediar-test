using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.Events;

namespace Core
{
    /// <summary>
    /// The firebase interface
    /// </summary>
    public class Authentication : MonoBehaviour
    {
        //Firebase variables
        [Header("Firebase")] public DependencyStatus dependencyStatus;
        private FirebaseAuth auth;

        public UnityAction loggedIn; //Event to open the Game scene

        void Awake()
        {
            //Check that all of the necessary dependencies for Firebase are present on the system
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    //If they are avalible Initialize Firebase
                    InitializeFirebase();
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        }

        private void InitializeFirebase()
        {
            //Set the authentication instance object
            auth = FirebaseAuth.DefaultInstance;
        }

        public IEnumerator Login(string _email, string _password, System.Action<string> callback)
        {
            //Call the Firebase auth signin function passing the email and password
            var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

            string result;

            if (LoginTask.Exception != null)
            {
                //If there are errors handle them
                FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError) firebaseEx.ErrorCode;

                result = "Login Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        result = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        result = "Missing Password";
                        break;
                    case AuthError.WrongPassword:
                        result = "Wrong Password";
                        break;
                    case AuthError.InvalidEmail:
                        result = "Invalid Email";
                        break;
                    case AuthError.UserNotFound:
                        result = "Account does not exist";
                        break;
                }
            }
            else
            {
                //User is now logged in
                result = "Logged In. Loading...";
                loggedIn();
            }

            callback(result);
        }

        public IEnumerator Register(string _email, string _password, System.Action<string> callback)
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            string result;
            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError) firebaseEx.ErrorCode;

                result = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        result = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        result = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        result = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        result = "Email Already In Use";
                        break;
                }
            }
            else
            {
                result = "Registered user";
            }

            callback(result);
        }
    }
}