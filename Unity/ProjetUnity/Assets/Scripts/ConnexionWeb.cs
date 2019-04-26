using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

public class ConnexionWeb : MonoBehaviour
{
    public GameObject gomail;
    public GameObject gopwd;

    private static string server = "http://34.76.34.147/";
    private static readonly HttpClient client = new HttpClient();
    private static string mail;
    private static string mdp;
    private DataManager data;
    private MenuLoader menu;

    private void Awake()
    {
        data = DataManager.GetInstance();
        menu = MenuLoader.instance;
    }

    public async void ButtonClick()
    {
        mail = gomail.GetComponent<InputField>().text;
        mdp = gopwd.GetComponent<InputField>().text;
        await RequetteAsync();
    }


    public async Task RequetteAsync()
    {
        string result = await RequetteHttpAsync();
        //Si bon resultat 
        JObject json = JObject.Parse(result);
        data.SetUser(json);
        await RequetteAsyncFormation(false);
        menu.GoodConnection();
    }

    public static async Task<string> RequetteHttpAsync()
    {
        string url = server +"GetUser/" + mail + "," + mdp;
        using (var result = await client.GetAsync($"{url}"))
        {
            string content = await result.Content.ReadAsStringAsync();
            return content;
        }
    }

    public async void FormationButon()
    {
        menu = MenuLoader.instance;
        await RequetteAsyncFormation(true);
    }

    public async Task RequetteAsyncFormation(bool canvasOpened)
    {
        Debug.Log(data.GetIdUser());
        string resultForm = await RequetteHttpAsyncFormation(data.GetIdUser());
        string resultCharacter = await RequetteHttpAsyncCharacter();
        JArray json = JArray.Parse(resultForm);

        Debug.Log(resultForm);
        JArray jsonCharacter = JArray.Parse(resultCharacter);
        menu.Formation(json, jsonCharacter, canvasOpened);
        int index = menu.GetIndexFormation();
        if (index == -1) { 
            menu.SetFormation(0, canvasOpened);
        }else{
            menu.SetFormation(index, canvasOpened);
        }
    }

  
    public static async Task<string> RequetteHttpAsyncCharacter()
    {
        string url = server + "GetCharacter";
        using (var result = await client.GetAsync($"{url}"))
        {
            string content = await result.Content.ReadAsStringAsync();
            return content;
        }
    }

    public static async Task<string> RequetteHttpAsyncFormation(string id)
    {
 
        string url = server + "GetFormation/" + id;
        using (var result = await client.GetAsync($"{url}"))
        {
            string content = await result.Content.ReadAsStringAsync();
            return content;
        }
    }

    public async void Deconnecter()
    {
        menu = MenuLoader.instance;
        await RequetteAsyncDeconnecter();
        menu.Deconnecter();
    }

    public async Task RequetteAsyncDeconnecter()
    {
        await RequetteHttpAsyncDesconnecter();
        data.Deconnecter();
    }

    public static async Task RequetteHttpAsyncDesconnecter()
    {
        string url = server + "DeleteSession";
        await client.GetAsync($"{url}");
    }
}