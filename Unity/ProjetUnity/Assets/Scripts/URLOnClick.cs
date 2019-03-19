using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLOnClick : MonoBehaviour
{
    public void clickUrl (string url)
    {
        Application.OpenURL(url);
    }
  
}
