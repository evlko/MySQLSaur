using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DBUser : MonoBehaviour
{
    public InputField username;
    public InputField password;

    public Button submitButton;

    public void SubmitAction(int buttonActionIndex)
    {
        StartCoroutine(SubmitActionToServer(buttonActionIndex));
    }
    
    private IEnumerator SubmitActionToServer(int buttonActionIndex)
    {
        ButtonAction buttonAction = (ButtonAction) buttonActionIndex;
        
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);

        WWW www = new WWW("");
        switch (buttonAction)
        {
            case ButtonAction.LogIn:
                www = new WWW("http://localhost:8888/sqlconnect/login.php", form);
                break;
            case ButtonAction.SignUp:
                www = new WWW("http://localhost:8888/sqlconnect/register.php", form);
                break;
        }

        yield return www;
        
        if (www.text[0] == '0')
        {
            print("Success");
            GameManager.instance.username = username.text;
            GameManager.instance.highscore = int.Parse(www.text.Split('\t')[1]);
            SceneManager.LoadScene("Main");
        }
        else
        {
            print("Action failed. error: #" + www.text);
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (username.text.Length >= 8 && password.text.Length >= 8);
    }

    public enum ButtonAction
    {
        LogIn = 0,
        SignUp = 1
    }
}
