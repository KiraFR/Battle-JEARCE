using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Formation : MonoBehaviour
{
    private DataManager data = DataManager.GetInstance();
    public GameObject canvas;
    public GameObject roulant;
    private MenuLoader menu;


    void Start()
    {
        menu = MenuLoader.instance;
    }

    public void SelectionFormation()
    {
        JObject user = data.GetUser();
        menu.SetFormation(roulant.GetComponent<Dropdown>().value,true);
        menu.SelectionFormation();
    }

    public void OnChangedValueFormation()
    {
        string formation = menu.GetFormation(roulant.GetComponent<Dropdown>().value);
        List<string> units = new List<string>(formation.Split(' '));
        for(int i = 0; i < units.Count; i++)
        {
            GameObject unitCanvas = canvas.transform.GetChild(i).gameObject;
            Sprite sprite = menu.GetSpriteFromName(units[i]);
            unitCanvas.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            unitCanvas.transform.GetChild(1).GetComponent<Text>().text = units[i];
        }
    }
}
