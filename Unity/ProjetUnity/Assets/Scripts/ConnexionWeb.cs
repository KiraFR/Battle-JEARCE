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

        /*string key = "fUjXn2r5u7x!A%D*";
        byte[] tabKey = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] data = Encoding.UTF8.GetBytes(mdp);
        byte[] enc = Encrypt(data, tabKey);
        string result = Encoding.UTF8.GetString(enc);*/

        await RequetteAsync();
    }


    public async Task RequetteAsync()
    {
        string result = await RequetteHttpAsync();
        //Si bon resultat 
        JObject json = JObject.Parse(result);

        Debug.Log(json);
        data.SetUser(json);
        await RequetteAsyncFormation(false);
        menu.GoodConnection();
    }

    /*public static byte[] Encrypt(byte[] data, byte[] key)
    {
        using (AesCryptoServiceProvider csp = new AesCryptoServiceProvider())
        {
            csp.Key = key;
            csp.Padding = PaddingMode.PKCS7;
            csp.Mode = CipherMode.ECB;
            ICryptoTransform encrypter = csp.CreateEncryptor();
            return encrypter.TransformFinalBlock(data, 0, data.Length);
        }
    }
    */

    public static async Task<string> RequetteHttpAsync()
    {
        string url = "http://localhost:5000/GetUser/" + mail + "," + mdp;
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
        string resultForm = await RequetteHttpAsyncFormation(data.GetIdUser());
        string resultCharacter = await RequetteHttpAsyncCharacter();
        JArray json = JArray.Parse(resultForm);

        Debug.Log(resultForm);
        JArray jsonCharacter = JArray.Parse(resultCharacter);
        menu.Formation(json, jsonCharacter);
        int index = menu.GetIndexFormation();
        if (index == -1) { 
            menu.SetFormation(0, canvasOpened);
        }else{
            menu.SetFormation(index, canvasOpened);
        }
    }

  
    public static async Task<string> RequetteHttpAsyncCharacter()
    {
        string url = "http://localhost:5000/GetCharacter";
        using (var result = await client.GetAsync($"{url}"))
        {
            string content = await result.Content.ReadAsStringAsync();
            return content;
        }
    }

    public static async Task<string> RequetteHttpAsyncFormation(string id)
    {
 
        string url = "http://localhost:5000/GetFormation/" + id;
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
        string url = "http://localhost:5000/DeleteSession";
        await client.GetAsync($"{url}");
    }
}