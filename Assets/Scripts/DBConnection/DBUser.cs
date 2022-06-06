using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DBUser : MonoBehaviour
{
    public InputField username;
    public InputField password;

    public Button submitButton;
    public TextMeshProUGUI errorText;
    
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
                print("log");
                www = new WWW("http://localhost:8888/sqlconnect/login.php", form);
                break;
            case ButtonAction.SignUp:
                www = new WWW("http://localhost:8888/sqlconnect/register.php", form);
                break;
        }

        yield return www;

        switch (www.text[0])
        {
            default:
                print("Action failed. error: #" + www.text);
                break;
            case '0':
                GameManager.instance.username = username.text;
                GameManager.instance.highscore = int.Parse(www.text.Split('\t')[1]);
                SceneManager.LoadScene("Main");
                break;
            case '3':
                errorText.text = "User with this name already exists";
                break;
            case '5':
                errorText.text = "No user with this name";
                break;
            case '6':
                errorText.text = "Incorrect password";
                break;
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (username.text.Length >= 8 && password.text.Length >= 8);
    }

    private enum ButtonAction
    {
        LogIn = 0,
        SignUp = 1
    }

    public void ClearErrorText()
    {
        errorText.text = "";
    }
}
