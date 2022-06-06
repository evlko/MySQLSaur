using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform menuNotLogged;
    public Transform menuLogged;
    
    private void Start()
    {
        if (GameManager.instance.username != null)
        {
            menuLogged.gameObject.SetActive(true);
        }
        else
        {
            menuNotLogged.gameObject.SetActive(true);
        }
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LogOut()
    {
        GameManager.instance.LogOut();
    }
}
