using Newtonsoft.Json.Linq;
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
    private List<string> formation;
    private string currentFormation = null;


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
        formation = new List<string>();

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

    public void Formation(JObject json,JArray jsonChara)
    {
        PlayPanel.SetActive(false);
        FormationPanel.SetActive(true);
        Component[] hingeJoints = FormationPanel.GetComponentsInChildren<Dropdown>();
        List<string> list = new List<string>();

        JArray formations = (JArray)json["formation"];



        for (int i = 0; i < formations.Count; i++)
        {
            int count = 0;
            string characters = "";
            JObject form = (JObject)json["formation"][i];
            for(int j = 0; j < form.Count; j++)
            {
                for (int v = 0; v < jsonChara.Count; v++)
                {
                    string characterFromForm = form["p" + (j + 1)].ToString();
                    string characterFromAll = jsonChara[v]["_id"].ToString();
                    if (characterFromForm == characterFromAll)
                    {
                        characters += jsonChara[v]["type"] + " ";
                        count++;
                    }
                }
            }
            if (count == 4)
            {
                list.Add("Formation : " + (i + 1));
                characters.TrimEnd();
                formation.Add(characters);
            }
        }
        hingeJoints[0].GetComponent<Dropdown>().AddOptions(list);
    }

    public void SelectionFormation()
    {
        PlayPanel.SetActive(true);
        FormationPanel.SetActive(false);
    }

    public void SetFormation(int index)
    {
        currentFormation = formation[index];
        Debug.Log(currentFormation);
    }
}
