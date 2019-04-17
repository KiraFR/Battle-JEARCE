using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;

public class ConnexionAplli : MonoBehaviour
{
    public GameObject gomail;
    public GameObject gopwd;

    private static readonly HttpClient client = new HttpClient();
    private string mail;
    private string mdp;

    public void ButtonClick()
    {
        mail = gomail.GetComponent<InputField>().text;
        mdp = gopwd.GetComponent<InputField>().text;

        /*string key = "fUjXn2r5u7x!A%D*";
        byte[] tabKey = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] data = Encoding.UTF8.GetBytes(mdp);

        byte[] enc = Encrypt(data, tabKey);
        string result = Encoding.UTF8.GetString(enc);*/

        RequetteHttpAsync();

    }

    public static byte[] Encrypt(byte[] data, byte[] key)
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

    public async System.Threading.Tasks.Task RequetteHttpAsync()
    {
        var values = new Dictionary<string, string> { { "mail", mail }, { "mdp", mdp } };
        var content = new FormUrlEncodedContent(values);
        var response = await client.PostAsync("http://localhost:8080", content);
        var responseString = await response.Content.ReadAsStringAsync();
    }
}