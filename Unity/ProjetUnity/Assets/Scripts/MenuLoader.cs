using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour
{
    public GameObject FormationPanel;
    public GameObject OptionsPanel;
    public GameObject PlayPanel;
    public GameObject MainMenuPanel;
    public GameObject ConnexionPanel;

    private DataManager data = DataManager.GetInstance();
    private SoundManager son;
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
        Component[] hingeJoints = OptionsPanel.GetComponentsInChildren<Slider>();
        hingeJoints[0].GetComponent<Slider>().value = data.getSetSfx();
        hingeJoints[1].GetComponent<Slider>().value = data.getVolume();

        if (data.Connecter())
        {
            PlayPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            son = SoundManager.instance;
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
        ConnexionPanel.SetActive(false);
        PlayPanel.SetActive(true);
    }

    public void Deconnecter()
    {
        PlayPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void Formation()
    {
        PlayPanel.SetActive(false);
        FormationPanel.SetActive(true);
    }
}
