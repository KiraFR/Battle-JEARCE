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


    public Sprite tank;
    public Sprite assassin;
    public Sprite guerrier;
    public Sprite pretre;
    public Sprite archer;

    public static MenuLoader instance = null;

    private DataManager data = DataManager.GetInstance();
    private SoundManager son;
    private List<string> formation;
    private string currentFormation = null;
    private int indexFormation = -1;

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
        hingeJoints[0].GetComponent<Slider>().value = data.GetSetSfx();
        hingeJoints[1].GetComponent<Slider>().value = data.GetVolume();

        if (data.Connecter())
        {
            PlayPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            son = SoundManager.instance;
            son.SetSfx(data.GetSetSfx());
            son.SetVolume(data.GetVolume());
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

    public void Formation(JArray formations, JArray jsonChara,bool canvasOpen)
    {

        List<string> list = new List<string>();

        for (int i = 0; i < formations.Count; i++)
        {
            int count = 0;
            string characters = "";

            JObject form = (JObject)formations[i];
            for (int j = 0; j < form.Count; j++)
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
                characters = characters.Trim();
                formation.Add(characters);
            }
        }
        if (canvasOpen)
        {
            PlayPanel.SetActive(false);
            FormationPanel.SetActive(true);
            Component[] hingeJoints = FormationPanel.GetComponentsInChildren<Dropdown>();
            hingeJoints[0].GetComponent<Dropdown>().AddOptions(list);
        }
    }

    public void SelectionFormation()
    {
        PlayPanel.SetActive(true);
        FormationPanel.SetActive(false);
    }

    public void SetFormation(int index,bool canvasOpened)
    {
        currentFormation = formation[index];
        data.SetFormation(currentFormation);
        indexFormation = index;
        if (canvasOpened)
        {
            SetFormation();
        }
    }

    public void SetFormation()
    {
        string form = GetFormation(indexFormation);
        List<string> units = new List<string>(form.Split(' '));
        GameObject canvasUnit = FormationPanel.transform.GetChild(1).gameObject;
        for (int i = 0; i < units.Count; i++)
        {
            GameObject unitCanvas = canvasUnit.transform.GetChild(i).gameObject;
            Sprite sprite = GetSpriteFromName(units[i]);
            unitCanvas.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            unitCanvas.transform.GetChild(1).GetComponent<Text>().text = units[i];
        }
    }

    public string GetFormation(int index)
    {
        return formation[index];
    }


    public Sprite GetSpriteFromName(string name)
    {
        switch (name)
        {
            case "Guerrier":
                return guerrier;
            case "Tank":
                return tank;
            case "Assassin":
                return assassin;
            case "Pretre":
                return pretre;
            case "Archer":
                return archer;
            default:
                return null;
        }
    }

    public int GetIndexFormation()
    {
        return indexFormation;
    }
}
