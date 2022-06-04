using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public string username;
    public bool loggedIn => username != null;

    private int _highscore;
    private int _maxhighscore;

    public int maxhighscore
    {
        get => _maxhighscore;
        set => _maxhighscore = value;
    }
    
    public int highscore
    {
        get => _highscore;
        set
        {
            _highscore = value;
            StartCoroutine(SavePlayerData());
        }
    }
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance == this) {
            Destroy(gameObject);
        }

        StartCoroutine(GetMaxHighscore());
        DontDestroyOnLoad(gameObject);
    }

    public void LogOut()
    {
        username = null;
    }

    private IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("highscore", _highscore);

        WWW www = new WWW("http://localhost:8888/sqlconnect/savedata.php", form);
        yield return www;
        if (www.text == "0")
        {
            print("Game Saved.");
        }
        else
        {
            print("Save failed. Error: #" + www.text);
        }
    }

    private IEnumerator GetMaxHighscore()
    {
        WWWForm form = new WWWForm();

        WWW www = new WWW("http://localhost:8888/sqlconnect/max_score.php", form);
        yield return www;
        if (www.text[0] == '0') 
        {
            _maxhighscore = int.Parse(www.text.Split('\t')[1]);
            print("Max highscore is set.");
        }
        else
        {
            print("Max highscore getter failed. Error: #" + www.text);
        }
    }
}
