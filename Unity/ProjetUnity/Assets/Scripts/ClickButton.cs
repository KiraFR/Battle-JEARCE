using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{

    public GameObject canvasForOptionCanvas;
    public GameObject OptionCanvas;
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
        buttonForOptionCanvas.GetComponent<Button>().onClick.RemoveAllListeners();
        buttonForOptionCanvas.GetComponent<Button>().onClick.AddListener(() => OptionCanvas.SetActive(false));
        buttonForOptionCanvas.GetComponent<Button>().onClick.AddListener(() => canvasForOptionCanvas.SetActive(true));
    }


    public void PlayButton()
    {
        if (!DataManager.GetInstance().Connecter()) { return; }
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
