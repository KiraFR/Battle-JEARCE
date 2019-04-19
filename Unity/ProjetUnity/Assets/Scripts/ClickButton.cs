using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{

    public GameObject canvasForOptionCanvas;
    public GameObject buttonForOptionCanvas;

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
    }

    public void clickUrl(string url)
    {
        Application.OpenURL(url);
    }


    public void BackButton()
    {
        buttonForOptionCanvas.GetComponent<Button>().onClick.AddListener(() => canvasForOptionCanvas.SetActive(true));
    }
}
