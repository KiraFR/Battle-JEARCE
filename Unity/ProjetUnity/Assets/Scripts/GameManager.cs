using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    public static GameManager instance = null;

    private GameObject selectedUnit;
    private List<GameObject> movingTiles;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        movingTiles = new List<GameObject>();
        InitGame();
    }

    void InitGame()
    {
        movingTiles.Clear();
        boardScript.SetupScene();
    }

    public GameObject GetGameObject(int xDir, int yDir)
    {
        return boardScript.GetGameObject(xDir, yDir);
    }

    public GameObject GetSelectedUnit()
    {
        return selectedUnit;
    }

    public void SetSelectedUnit(GameObject unit)
    {
        selectedUnit = unit;
    }

    public void AddMovingTiles(GameObject tile)
    {
        movingTiles.Add(tile);
    }

    public void ClearMovingTiles()
    {
        foreach(GameObject obj in movingTiles){
            obj.GetComponent<SpriteRenderer>().color = obj.GetComponent<Square>().baseColor;
        }
        movingTiles.Clear();
    }

}