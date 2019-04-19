using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class DataManager
{
    public static DataManager instance = null;
    private DataManager() {}

    public static DataManager GetInstance()
    {
        if(instance == null)
        {
            instance = new DataManager();
        }
        return instance;
    }

    public void VerifFichier()
    {
        if (!File.Exists("data.txt"))
        {
            string[] ini = { "", "", "0.5", "0.5" };
            File.WriteAllLines("data.txt", ini);
        }
    }

    public void EcrirePseudoMail(string pseudo, string mail)
    {
        string[] ancien = File.ReadAllLines("data.txt");
        ancien[0] = pseudo;
        ancien[1] = mail;
        File.WriteAllLines("data.txt", ancien);
    }

    public void EcrireSonMusic(string son, string music)
    {
        string[] ancien = File.ReadAllLines("data.txt");
        ancien[2] = son;
        ancien[3] = music;
        File.WriteAllLines("data.txt", ancien);
    }

    public bool Connecter()
    {
        string[] ancien = File.ReadAllLines("data.txt");
        if (ancien[0] != "" && ancien[1] != "")
            return true;
        else
            return false;
    }


    public float getVolume()
    {
        string[] ancien = File.ReadAllLines("data.txt");
        Debug.Log(ancien[2]);
        return float.Parse(ancien[2], CultureInfo.InvariantCulture.NumberFormat);

    }

    public float getSetSfx()
    {
        string[] ancien = File.ReadAllLines("data.txt");
        return float.Parse(ancien[3], CultureInfo.InvariantCulture.NumberFormat);

    }
}