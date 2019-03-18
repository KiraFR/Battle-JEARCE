using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    public static GameManager instance = null;
    public GameObject canvasAction = null;


    private GameObject instantiatedCanvasAction = null;
    private GameObject selectedUnit = null;
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

    public void SetInstantiatedCanvasAction(GameObject canvas)
    {
        RemoveInstantiatedCanvasAction();

        instantiatedCanvasAction = canvas;
    }

    public void RemoveInstantiatedCanvasAction()
    {
        if(instantiatedCanvasAction != null)
        {
            Destroy(instantiatedCanvasAction);
            instantiatedCanvasAction = null;
        }
    }

    public GameObject GetinstantiatedCanvasAction()
    {
        return instantiatedCanvasAction;
    }

    public void AddMovingTiles(GameObject tile)
    {
        movingTiles.Add(tile);
    }

    public void ClearMovingTiles()
    {
        foreach(GameObject obj in movingTiles){
            obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<Square>().baseSprite;
            obj.GetComponent<Square>().SetMovable(false);
        }
        movingTiles.Clear();
    }

}