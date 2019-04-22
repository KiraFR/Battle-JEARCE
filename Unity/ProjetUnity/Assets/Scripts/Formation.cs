﻿using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Formation : MonoBehaviour
{
    private DataManager data = DataManager.GetInstance();

    public GameObject roulant;
    private MenuLoader menu;
    private JObject selection = null;

    public void SelectionFormation()
    {
        menu = MenuLoader.instance;
        JObject user = data.getUser();
        menu.SetFormation(((JObject)user["formation"][roulant.GetComponent<Dropdown>().value]));
        menu.SelectionFormation();
    }
}
