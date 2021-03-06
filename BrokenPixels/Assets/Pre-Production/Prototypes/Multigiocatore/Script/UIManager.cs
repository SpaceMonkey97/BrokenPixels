﻿using UnityEngine;
using UnityEngine.UI;

[System.Obsolete]
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public GameObject startMenu;
    public InputField usernameField;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ConnectedToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        UnityClient.instance.ConnectedToServer();
    }
}
