﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour
{
    public GameObject FormationPanel;
    public GameObject OptionsPanel;
    public GameObject PlayPanel;
    public GameObject MainMenuPanel;
    public GameObject ConnexionPanel;

    DataManager data = DataManager.GetInstance();
    public static MenuLoader instance = null;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        FormationPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        ConnexionPanel.SetActive(false);


        data.VerifFichier();
        if (data.Connecter())
        {
            PlayPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            SoundManager son = SoundManager.instance;
            son.SetSfx(data.getSetSfx());
            son.SetVolume(data.getVolume());

        }
        else
        {
            MainMenuPanel.SetActive(true);
            PlayPanel.SetActive(false);
        }
    }

    public void GoodConnection()
    {
        MainMenuPanel.SetActive(false);
        PlayPanel.SetActive(true);
    }
}