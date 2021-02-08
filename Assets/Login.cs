using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public string email;
    public Text txtEmail;

    public string password;
    public Text txtPassword;


    public void LoginPinaka(){
        email = txtEmail.text.ToString();
        password = txtPassword.text.ToString();
    }
}
