using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    public static GameManager instance = null;
    public GameObject canvasAction = null;

    private List<GameObject> enemies;
    private List<GameObject> allies;
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
        enemies = new List<GameObject>();
        allies = new List<GameObject>();
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
        if (instantiatedCanvasAction != null)
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
        if (tile != null)
        {
            tile.GetComponent<SpriteRenderer>().sprite = tile.GetComponent<Square>().moveSprite;
            tile.GetComponent<Square>().SetMovable(true);
            movingTiles.Add(tile);
        }
    }

    public void ClearMovingTiles()
    {
        foreach (GameObject obj in movingTiles)
        {
            obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<Square>().baseSprite;
            obj.GetComponent<Square>().SetMovable(false);
        }
        movingTiles.Clear();
    }

    public void AddToEnemies(GameObject unit)
    {
        if (!enemies.Contains(unit))
        {
            enemies.Add(unit);
        }
    }

    public void AddToAllies(GameObject unit)
    {
        if (!allies.Contains(unit))
        {
            allies.Add(unit);
        }
    }

    public bool IsEnemy(GameObject unit)
    {
        return enemies.Contains(unit);
    }
    public bool IsAlly(GameObject unit)
    {
        return allies.Contains(unit);
    }

    public void MovableSquares(int posX, int posY, int mouvement)
    {
        if (mouvement == 0) return;
        GameObject objet = GetGameObject(posX + 1, posY);
        if (objet != null && !selectedUnit.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    AddMovingTiles(objet);
                    MovableSquares(posX + 1, posY, mouvement - 1);
                }
            }
        }
        objet = GetGameObject(posX - 1, posY);
        if (objet != null && !selectedUnit.Equals(objet))
        {

            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    AddMovingTiles(objet);
                    MovableSquares(posX - 1, posY, mouvement - 1);
                }
            }
        }
        objet = GetGameObject(posX, posY + 1);
        if (objet != null && !selectedUnit.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
            {

                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    AddMovingTiles(objet);
                    MovableSquares(posX, posY + 1, mouvement - 1);
                }
            }
        }
        objet = GetGameObject(posX, posY - 1);
        if (objet != null && !selectedUnit.Equals(objet))
        {
            if (objet.GetComponent<SpriteRenderer>().sprite == objet.GetComponent<Square>().baseSprite)
            {
                Character character = objet.GetComponent<Square>().GetCharacter();
                if ((character != null && !IsEnemy(character.gameObject)) || character == null)
                {
                    AddMovingTiles(objet);
                    MovableSquares(posX, posY - 1, mouvement - 1);
                }
            }
        }
    }

}