using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour
{
    public GameObject FormationPanel;
    public GameObject OptionsPanel;
    public GameObject PlayPanel;
    public GameObject MainMenuPanel;
    public GameObject ConnexionPanel;


    void Start()
    {
        FormationPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        PlayPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        ConnexionPanel.SetActive(false);
    }
}
