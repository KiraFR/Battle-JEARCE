using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class ConnexionAplli : MonoBehaviour
{
    public GameObject gomail;
    public GameObject gopwd;

    private static readonly HttpClient client = new HttpClient();
    private static string mail;
    private static string mdp;
    DataManager data;
    private MenuLoader menu;

    public void ButtonClick()
    {
        mail = gomail.GetComponent<InputField>().text;
        mdp = gopwd.GetComponent<InputField>().text;

        /*string key = "fUjXn2r5u7x!A%D*";
        byte[] tabKey = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] data = Encoding.UTF8.GetBytes(mdp);
        byte[] enc = Encrypt(data, tabKey);
        string result = Encoding.UTF8.GetString(enc);*/

        RequetteAsync();
        data = DataManager.GetInstance();
        menu = MenuLoader.instance;
        menu.GoodConnection();
    }


    public async Task RequetteAsync()
    {
        string result = await RequetteHttpAsync();


        //Si bon resultat 
        JObject json = JObject.Parse(result);
        Debug.Log(json);
        
        data.EcrirePseudoMail((string)json["pseudo"], (string)json["email"]);

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
    }*/

    public static async Task<string> RequetteHttpAsync()
    {
        string url = "http://localhost:5000/GetUser/" + mail + "," + mdp;
        using (var result = await client.GetAsync($"{url}"))
        {
            string content = await result.Content.ReadAsStringAsync();
            return content;
        }
    }
}