using UnityEngine;
using UnityEngine.UI;
using Core;

namespace PokeUI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private InputField inpEmail;
        [SerializeField] private InputField inpPassword;

        [SerializeField] private Text txtNotification;

        [SerializeField] private Authentication auth; //the firebase instance

        public void LoginButton()
        {
            //call the courutine and return a message with the result, if the login is success, just call a event in the auth class
            StartCoroutine(auth.Login(inpEmail.text, inpPassword.text, (result) => { txtNotification.text = result; }));
        }

        public void RegisterButton()
        {
            //call the courutine and return a message with the result
            StartCoroutine(auth.Register(inpEmail.text, inpPassword.text,
                (result) => { txtNotification.text = result; }));
        }
    }
}