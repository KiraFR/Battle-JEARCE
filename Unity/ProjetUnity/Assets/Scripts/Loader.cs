using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    void Start()
    {
        float targetaspect = 9.0f / 18.0f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
      
        float scaleheight = windowaspect / targetaspect;
        Camera camera = GetComponent<Camera>();
        if (scaleheight > 1.0f)
        {
            camera.orthographicSize = 5.4f;
        }
    }

    void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
