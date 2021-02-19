using UnityEngine;
using UnityEngine.UI;

public class MenuUI   : MonoBehaviour
{
    [SerializeField]
    private InputField inpEmail;
    [SerializeField]
    private InputField inpPassword;
    
    [SerializeField]
    private Text txtNotification;

    [SerializeField]
    private Authentication auth;

    public void LoginButton()
    {
        
        //Call the login coroutine passing the email and password
        StartCoroutine(auth.Login(inpEmail.text, inpPassword.text, (result) =>
        {
            txtNotification.text = result;
        }));
        
    }
    
    public void RegisterButton()
    {
        
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(auth.Register(inpEmail.text, inpPassword.text, (result) =>
        {
            txtNotification.text = result;
        }));
        
    }
    
}