using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField emailField;
    public InputField passwordField;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        emailField.interactable = false;
        passwordField.interactable = false;

        
        Client.instance.ConnectToServer();
        Loader.LoadLevel(Loader.Scene.LoadingSpinner, Loader.Scene.Menu);
    }
}