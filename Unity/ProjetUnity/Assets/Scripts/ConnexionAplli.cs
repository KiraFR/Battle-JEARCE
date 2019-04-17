using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
public class ConnexionAplli : MonoBehaviour
{
    public GameObject gomail;
    public GameObject gopwd;

    public void ButtonClick()
    {
        string mail = gomail.GetComponent<InputField>().text;
        string mdp = gopwd.GetComponent<InputField>().text;

        string key = "fUjXn2r5u7x!A%D*";
        byte[] tabKey = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] data = Encoding.UTF8.GetBytes(mdp);

        byte[] enc = Encrypt(data, tabKey);

        string result = System.Text.Encoding.UTF8.GetString(enc);
        Debug.Log(result);
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
}