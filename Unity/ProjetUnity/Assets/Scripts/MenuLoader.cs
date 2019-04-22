using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
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
    private JObject formation;


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

        OptionsPanel.SetActive(false);
        ConnexionPanel.SetActive(false);
        FormationPanel.SetActive(false);


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

    public void Formation(JObject json)
    {
        PlayPanel.SetActive(false);
        FormationPanel.SetActive(true);
        Component[] hingeJoints = FormationPanel.GetComponentsInChildren<Dropdown>();
        List<string> list = new List<string>();
        for(int i=0;i<= ((JArray)json["formation"]).Count; i++) 
        {
            list.Add("Formation : "+(i+1));
        }
        hingeJoints[0].GetComponent<Dropdown>().AddOptions(list);
    }

    public void SelectionFormation()
    {
        PlayPanel.SetActive(true);
        FormationPanel.SetActive(false);
    }

    public void SetFormation(JObject f)
    {
        formation = f;
    }
}
